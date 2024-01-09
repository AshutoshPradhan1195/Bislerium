using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium.Components.Data
{
    public class AddOns
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public short Price { get; set; }
    }
}
