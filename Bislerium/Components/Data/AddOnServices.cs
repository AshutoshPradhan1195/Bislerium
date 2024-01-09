using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bislerium.Components.Data
{
    public class AddOnServices
    {
        private static void SaveAll(List<AddOns> addOn)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appAddOnFilePath = Utils.GetAppAddOnDirectoryPath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(addOn);
            File.WriteAllText(appAddOnFilePath, json);
        }

        public static List<AddOns> GetAll()
        {
            string appAddOnFilePath = Utils.GetAppAddOnDirectoryPath();
            if (!File.Exists(appAddOnFilePath))
            {
                return new List<AddOns>();
            }

            var json = File.ReadAllText(appAddOnFilePath);

            return JsonSerializer.Deserialize<List<AddOns>>(json);
        }

        public static List<AddOns> Create(Guid id, string name, float price)
        {
            List<AddOns> addOns = GetAll();
            bool addOnsExists = addOns.Any(x => x.Id == id);

            if (addOnsExists)
            {
                throw new Exception("AddOn already exists.");
            }

            addOns.Add(
                new AddOns
                {
                    Name = name,
                    Price = price
                }
            ); ; ;
            SaveAll(addOns);
            return addOns;
        }

        public static AddOns GetById(Guid id)
        {
            List<AddOns> addOns = GetAll();
            return addOns.FirstOrDefault(x => x.Id == id);
        }

        public static List<AddOns> Delete(Guid id)
        {
            List<AddOns> addOns = GetAll();
            AddOns addOn = addOns.FirstOrDefault(x => x.Id == id);

            if (addOn == null)
            {
                throw new Exception("AddOn not found.");
            }

            addOns.Remove(addOn);
            SaveAll(addOns);

            return addOns;
        }
    }
}
