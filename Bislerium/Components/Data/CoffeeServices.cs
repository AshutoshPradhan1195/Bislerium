using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bislerium.Components.Data
{
    public class CoffeeServices { 

        private static void SaveAll(List<Coffee> coffees)
    {
        string appDataDirectoryPath = Utils.GetAppDirectoryPath();
        string appCoffeeFilePath = Utils.GetAppCoffeeFilePath();

        if (!Directory.Exists(appDataDirectoryPath))
        {
            Directory.CreateDirectory(appDataDirectoryPath);
        }

        var json = JsonSerializer.Serialize(coffees);
        File.WriteAllText(appCoffeeFilePath, json);
    }

    public static List<Coffee> GetAll()
    {
            string appCoffeeFilePath = Utils.GetAppCoffeeFilePath();
            if (!File.Exists(appCoffeeFilePath))
        {
            return new List<Coffee>();
        }

        var json = File.ReadAllText(appCoffeeFilePath);

        return JsonSerializer.Deserialize<List<Coffee>>(json);
    }

    public static List<Coffee> Create(Guid id, string name, float price)
    {
        List<Coffee> coffees = GetAll();
        bool coffeeExists = coffees.Any(x => x.Id == id);

        if (coffeeExists)
        {
            throw new Exception("Coffee already exists.");
        }

        coffees.Add(
            new Coffee
            {
                Name = name,
                Price = price
            }
        ); ; ;
        SaveAll(coffees);
        return coffees;
    }

    public static Coffee GetById(Guid id)
    {
        List<Coffee> coffees = GetAll();
        return coffees.FirstOrDefault(x => x.Id == id);
    }

        public static void changePrice(Guid id, double price)
        {
        
            List<Coffee> coffees = GetAll();
            Coffee coffee = coffees.FirstOrDefault(x => x.Id == id);

            coffee.Price = price;
            SaveAll(coffees);

        }
        public static List<Coffee> Delete(Guid id)
    {
        List<Coffee> coffees = GetAll();
            Coffee coffee = coffees.FirstOrDefault(x => x.Id == id);

        if (coffee == null)
        {
            throw new Exception("Coffee not found.");
        }

        coffees.Remove(coffee);
        SaveAll(coffees);

        return coffees;
    }

}
}
