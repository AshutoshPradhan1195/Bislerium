using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium.Components.Data
{
    public class Coffee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public double Price { get; set; }

        public DateTime addedOn { get; set; } = DateTime.Now;
    }
}
