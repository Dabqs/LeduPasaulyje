using LeduPasaulyjeData.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeduPasaulyjeData.Library.Models
{
    public class StoreModel : IEquatable<StoreModel>
    {
        public string Name { get; set; }
        public string CompanyNumber { get; set; }
        public string VatNumber { get; set; }
        public string Address { get; set; }
        public List<RegionModel> Regions { get; set; } = new List<RegionModel>();
        private List<string> availableRegions = new List<string>();


        public StoreModel(string name, string companyNumber, string vatNumber, string address, List<RegionModel> regions)
        {
            Name = name;
            Address = address;
            CompanyNumber = companyNumber;
            VatNumber = vatNumber;
            Regions = regions;
        }

        public bool Equals(StoreModel other)
        {
            bool isEqual = false;
            isEqual = Name.Trim().ToLower() == other.Name.Trim().ToLower() &&
                CompanyNumber.Trim().ToLower() == other.CompanyNumber.Trim().ToLower() &&
                VatNumber.Trim().ToLower() == other.VatNumber.Trim().ToLower() &&
                Address.Trim().ToLower() == other.Address.Trim().ToLower() &&
                Regions.Equals(other.Regions);
                

            //if (isEqual)
            //{
            //   
            //}
            return isEqual;

        }
    }
}

