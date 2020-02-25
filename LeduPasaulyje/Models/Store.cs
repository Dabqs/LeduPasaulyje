using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeduPasaulyje
{
   public class Store
    {
        public string Region { get; set; }
        public string Name { get; set; }
        public uint CompanyNumber { get; set; }
        public string VatNumber { get; set; }
        public string Address { get; set; }
        public Store(string region, string name, uint companyNumber, string vatNumber, string address)
        {
            Region = region;
            Name = name;
            CompanyNumber = companyNumber;
            VatNumber = vatNumber;
            Address = address;
        }

    }
}
