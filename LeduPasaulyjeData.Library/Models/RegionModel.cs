using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeduPasaulyjeData.Library.Models
{
    public class RegionModel 
    {
        public string Region { get; set; }
        public bool IsSelected { get; set; }
        public RegionModel(string region, bool isSelected = false)
        {
            Region = region;
            IsSelected = isSelected;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((RegionModel)obj);
        }
        private bool Equals(RegionModel other)
        {
            if (other == null)
                return false;
            return other.IsSelected == IsSelected &&
                other.Region == Region; 
        }
    }
}
