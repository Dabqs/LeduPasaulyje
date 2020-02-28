using LeduPasaulyjeData.Library.Models;
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
        // public string Category { get; set; }
        public string Name { get; set; }
        public uint AmountInBox { get; set; }
        public decimal Price { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public CategoryModel SelectedCategory { get; set; }


        public ProductModel(ulong barcode, CategoryModel category, string name, uint amountInBox, decimal price)
        {
            Categories = new List<CategoryModel>() { new CategoryModel() { Category = "Ledai" }, new CategoryModel() { Category = "Šaldyti produktai" } };
            //Categories = new List<CategoryModel>();
            Barcode = barcode;
            SelectedCategory = category;
            Name = name;
            AmountInBox = amountInBox;
            Price = price;
        }

        public bool Equals(ProductModel other)
        {
            return Barcode == other.Barcode &&
                //Category == other.Category && 
                Name == other.Name &&
                AmountInBox == other.AmountInBox &&
                Price == other.Price;
        }
    }
}
