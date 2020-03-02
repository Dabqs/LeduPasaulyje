using LeduPasaulyjeData.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeduPasaulyje.Models
{
    public class ProductModel : IEquatable<ProductModel>
    {
        //Choosing string because of the ease of entering values into TextBoxes (implemented custom validation)
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string AmountInBox { get; set; }
        public string Price { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public CategoryModel SelectedCategory { get; set; }
        private char systemDefaultDecimalSeparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        private string validatedPrice;

        public ProductModel(string barcode, CategoryModel category, string name, string amountInBox, string price)
        {
            validatedPrice = price.Replace('.', systemDefaultDecimalSeparator).Replace(',', systemDefaultDecimalSeparator); //if system has different decimal point than the development machine, this conversion makes sure that calculations will work correctly afterwards.
            Price = validatedPrice;
            Categories = new List<CategoryModel>() { new CategoryModel() { Category = "Ledai" }, new CategoryModel() { Category = "Šaldyti produktai" } };
            Barcode = barcode;
            SelectedCategory = category;
            Name = name;
            AmountInBox = amountInBox;
        }

        public bool Equals(ProductModel other)
        {
            return Barcode == other.Barcode &&
                SelectedCategory.Category == other.SelectedCategory.Category && 
                Name == other.Name &&
                AmountInBox == other.AmountInBox &&
                Price == other.Price;
        }
    }
}
