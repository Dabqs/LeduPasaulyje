using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeduPasaulyje.Models
{
   public class ProductModel : IEquatable<ProductModel>
    {
        public ulong Barcode { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public uint AmountInBox { get; set; }
        public decimal Price { get; set; }
        public ProductModel(ulong barcode, string category, string name, uint amountInBox, decimal price)
        {
            Barcode = barcode;
            Category = category;
            Name = name;
            AmountInBox = amountInBox;
            Price = price;
        }

        public bool Equals(ProductModel other)
        {
            return Barcode == other.Barcode && 
                Category == other.Category && 
                Name == other.Name && 
                AmountInBox == other.AmountInBox &&
                Price == other.Price;
        }
    }
}
