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
           // GetAvailableRegions();
            Name = name;
            Address = address;
            CompanyNumber = companyNumber;
            VatNumber = vatNumber;
            Regions = regions;
            

        }
       // private void GetAvailableRegions()
       // {
       //     foreach (var item in Enum.GetValues(typeof(AvailableRegions)))
       //     {
       //         availableRegions.Add(item.ToString());
       //     }
       // }
        public bool Equals(StoreModel other)
        {
            return Name == other.Name &&
                //SelectedRegion.Region == other.SelectedRegion.Region &&
                CompanyNumber == other.CompanyNumber &&
                VatNumber == other.VatNumber;
        }
    }
}

