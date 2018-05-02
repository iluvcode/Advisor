using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Advisor.Entities
{
    public  class Contractor
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public bool Certified { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }
    }
}
