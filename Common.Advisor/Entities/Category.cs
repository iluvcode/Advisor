using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Advisor.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string ServiceType { get; set; }

        public bool  Active { get; set; }

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; }

    }
}
