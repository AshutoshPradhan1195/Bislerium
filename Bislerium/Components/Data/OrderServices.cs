using System.Text.Json;

namespace Bislerium.Components.Data
{
    public class OrderServices
    {
        private static void SaveAll(List<Orders> orders)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appOrdersFilePath = Utils.GetPurchasesDirectoryPath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(orders);
            File.WriteAllText(appOrdersFilePath, json);
        }

        public static List<Orders> GetAll()
        {
            string appOrdersFilePath = Utils.GetPurchasesDirectoryPath();
            if (!File.Exists(appOrdersFilePath))
            {
                return new List<Orders>();
            }

            var json = File.ReadAllText(appOrdersFilePath);

            return JsonSerializer.Deserialize<List<Orders>>(json);
        }

        public static List<Orders> Create(Guid id, string memberId, string phoneNo, List<string> CoffeeId, List<string> AddOnId, double Price, Boolean isFreeCoffee, List<string> CoffeeName, List<string> AddOnName)
        {
            List<Orders> orders = GetAll();
            string name;

            if (phoneNo != "0")
            {
                 name = MemberService.GetByPhoneNo(phoneNo).FullName;

            }
            else
            {
                name = "";
            }

            orders.Add(
                new Orders
                {
                    MemberId = memberId,
                    MemberName =  name,
                    PhoneNumber = phoneNo,
                    CoffeeId = CoffeeId,
                    CoffeeName = CoffeeName,
                    AddOnName = AddOnName,
                    AddOnId = AddOnId,
                    Price   = Price,
                    isFreeCoffee = isFreeCoffee
                }
            );;; ; ;
            SaveAll(orders);
            return orders;
        }

        public static List<Orders> GetAllByPhoneNumber(String phoneNo)
        {
            List<Orders> orders = GetAll();
            return orders.FindAll(x => x.PhoneNumber == phoneNo);
        }
    }
}
