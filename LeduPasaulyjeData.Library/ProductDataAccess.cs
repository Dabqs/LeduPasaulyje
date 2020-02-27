﻿using LeduPasaulyje.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeduPasaulyjeData.Library
{
    public class ProductDataAccess
    {
        readonly string productsJsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Products.json");
        public List<ProductModel> GetAllProducts()
        {
            return JsonConvert.DeserializeObject<List<ProductModel>>(GetJsonString());
        }

        public void UpdateProductsList(ProductModel product)
        {
            List<ProductModel> existingProducts = GetAllProducts();

            if (!IdenticalProductExists(product, existingProducts))
            {
                if (MatchingProductExists(product, existingProducts))
                {//Edit existing product
                    EditProduct(product, existingProducts);
                }
                else
                {
                    AddProduct(product);
                }
            }

        }
        private void EditProduct(ProductModel product, List<ProductModel> existingProducts)
        {
            int productIndex = existingProducts.IndexOf(GetMatchingProduct(product.Name, product.Category, existingProducts));
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(GetJsonString());
            jsonObj[productIndex]["AmountInBox"] = product.AmountInBox;
            jsonObj[productIndex]["Price"] = product.Price;
            jsonObj[productIndex]["Barcode"] = product.Barcode;

            string jsonOutput = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(productsJsonFilePath, jsonOutput);

        }
        private ProductModel GetMatchingProduct(string name, string category, List<ProductModel> existingProducts)
        {
            return existingProducts.Where(product => product.Name == name && product.Category == category).FirstOrDefault();
        }

        public void RemoveProduct(ProductModel product)
        {
            List<ProductModel> existingProducts = GetAllProducts();
            if (IdenticalProductExists(product, existingProducts))
            {
                List<ProductModel> updatedProducts = existingProducts.Where(p => p.Name != product.Name || p.Category != product.Category).ToList();
                string jsonOutput = JsonConvert.SerializeObject(updatedProducts, Formatting.Indented);
                File.WriteAllText(productsJsonFilePath, jsonOutput);
            }
        }

        public void AddProduct(ProductModel product)
        {
            JArray array = JArray.Parse(GetJsonString());
            JObject itemToAdd = new JObject();

            itemToAdd["Category"] = product.Category;
            itemToAdd["Name"] = product.Name;
            itemToAdd["AmountInBox"] = product.AmountInBox;
            itemToAdd["Price"] = product.Price;
            itemToAdd["Barcode"] = product.Barcode;
            array.Add(itemToAdd);

            string jsonOutput = JsonConvert.SerializeObject(array, Formatting.Indented);
            File.WriteAllText(productsJsonFilePath, jsonOutput);
        }
        private bool MatchingProductExists(ProductModel product, List<ProductModel> existingProducts)
        {
            //if matching product found, return true
            return GetMatchingProduct(product.Name, product.Category, existingProducts) != null;
        }
        private bool IdenticalProductExists(ProductModel product, List<ProductModel> existingProducts)
        {
            return existingProducts.Contains(product);
        }
        private string GetJsonString()
        {
            try
            {
                return File.ReadAllText(productsJsonFilePath);
            }
            catch (Exception)
            {
                if (!File.Exists(productsJsonFilePath))
                {
                    throw new FileNotFoundException($"Nepavyko įkelti produktų. Failas \"{productsJsonFilePath}\" nerastas.");
                }
                else
                {
                    throw new Exception($"Nepavyko įkelti produktų. Patikrinkite, ar failas \"{productsJsonFilePath}\" nėra atidarytas. Jeigu yra, uždarykite jį.");
                }
            }
        }
    }
}
