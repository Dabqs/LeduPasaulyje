using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LeduPasaulyjeData.Library
{
    public class Produktas
    {
        public ulong Barcode { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public uint AmountInBox { get; set; }
        public decimal Price { get; set; }
    }
    public class Class1
    {







        Produktas produktas = new Produktas() {
        Barcode = 46545,
        Category = "IceCream",
        Name = "Pols plombyras",
        AmountInBox = 40,
        Price = 1.58M
        };

        public void Testas()
        {
            string initialJson = "[{\"Category\":\"IceCream\",\"Name\":\"Pols\", \"AmountInBox\":40,\"Price\":1.57,\"Barcode\":5464884}]";

            var array = JArray.Parse(initialJson);
            JObject itemToAdd = new JObject();
            itemToAdd["Category"] = "IceCream";
            itemToAdd["Name"] = "Twist citrininiai";
            itemToAdd["AmountInBox"] = 39;
            itemToAdd["Price"] = 0.78;
            itemToAdd["Barcode"] = 55549987;

            array.Add(itemToAdd);

            string jsonOutput = JsonConvert.SerializeObject(array, Formatting.Indented);
            File.WriteAllText("Test.txt", jsonOutput);
        }
       // public List<ProductModel> PopulateProducts()
       // {
       //     List<Produktas> produktai = new List<Produktas>();
       //     string jsonString = File.ReadAllText("Test.txt");
       //     return JsonConvert.DeserializeObject<List<ProductModel>>(jsonString);
       // }
   
    }
}

