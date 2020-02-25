using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeduPasaulyje.Models
{
   public class Product
    {

        public ulong Barcode { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public uint AmountInBox { get; set; }
        public decimal Price { get; set; }


        public Product(ulong barcode, string category, string name, uint amountInBox, decimal price)
        {
            Barcode = barcode;
            Category = category;
            Name = name;
            AmountInBox = amountInBox;
            Price = price;
        }
    }
}
