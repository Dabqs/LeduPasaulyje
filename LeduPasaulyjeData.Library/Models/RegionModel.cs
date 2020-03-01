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
    }
}
