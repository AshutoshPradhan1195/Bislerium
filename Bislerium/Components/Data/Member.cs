using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium.Components.Data
{
    public class Member
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FullName { get; set; }
        public string PhoneNo { get; set; }
        public DateTime MembershipDate { get; set; } = DateTime.Now;

    }
}
