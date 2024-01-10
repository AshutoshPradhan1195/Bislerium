using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium.Components.Data
{
    public class Orders
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string MemberId { get; set; }

        public string MemberName { get; set; }

        public string PhoneNumber { get; set; }

        public List<string> CoffeeId { get; set; }

        public List<string> CoffeeName { get; set; }


        public double Price { get; set; }

        public List<string> AddOnId { get; set; }

        public List<string> AddOnName { get; set; }


        public DateTime OrderDate { get; set;} = DateTime.Now;

        public Boolean isFreeCoffee { get; set; } = false;
    }
}
