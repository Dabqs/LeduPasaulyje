using LeduPasaulyjeData.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeduPasaulyjeData.Library.Models
{
    public class StoreModel :IEquatable<StoreModel>
    {
        public string Name { get; set; }
        public string CompanyNumber { get; set; }
        public string VatNumber { get; set; }
        public List<RegionModel> Regions { get; set; }
        public RegionModel SelectedRegion { get; set; }

        public StoreModel(string name, string companyNumber, string vatNumber, RegionModel selectedRegion)
        {
            Name = name;
            CompanyNumber = companyNumber;
            VatNumber = vatNumber;
            SelectedRegion = selectedRegion;
            foreach (var item in Enum.GetValues(typeof(AvailableRegions)))
            {
                Regions.Add(new RegionModel() { Region = item.ToString() });
            }
        }

        public bool Equals(StoreModel other)
        {
            return Name == other.Name &&
                SelectedRegion.Region == other.SelectedRegion.Region &&
                CompanyNumber == other.CompanyNumber &&
                VatNumber == other.VatNumber;
        }
    }
}
