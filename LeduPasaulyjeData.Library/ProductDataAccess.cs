using LeduPasaulyje.Models;
using LeduPasaulyjeData.Library.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeduPasaulyjeData.Library
{
    public class ProductDataAccess
    {
        private readonly string productsJsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Products.json");
        public List<ProductModel> GetAllProducts()
        {
            return JsonConvert.DeserializeObject<List<ProductModel>>(JsonHelpers.GetJsonString(productsJsonFilePath));
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
            int productIndex = existingProducts.IndexOf(GetMatchingProduct(product.Name, existingProducts));
            dynamic jsonObj = JsonConvert.DeserializeObject(JsonHelpers.GetJsonString(productsJsonFilePath));
            jsonObj[productIndex]["AmountInBox"] = product.AmountInBox;
            jsonObj[productIndex]["SelectedCategory"]["Category"] = product.SelectedCategory.Category;
            jsonObj[productIndex]["Price"] = product.Price;
            jsonObj[productIndex]["Barcode"] = product.Barcode;

            string jsonOutput = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(productsJsonFilePath, jsonOutput);

        }
        private ProductModel GetMatchingProduct(string name, List<ProductModel> existingProducts)
        {
            return existingProducts.Where(product => product.Name == name).FirstOrDefault();
        }

        public void RemoveProduct(ProductModel product)
        {
            List<ProductModel> existingProducts = GetAllProducts();
            if (IdenticalProductExists(product, existingProducts))
            {
                List<ProductModel> updatedProducts = existingProducts.Where(p => p.Name.Trim().ToLower() != product.Name.Trim().ToLower()
                || p.SelectedCategory.Category.Trim().ToLower() != product.SelectedCategory.Category.Trim().ToLower()).ToList();
                string jsonOutput = JsonConvert.SerializeObject(updatedProducts, Formatting.Indented);
                File.WriteAllText(productsJsonFilePath, jsonOutput);
            }
        }

        public void AddProduct(ProductModel product)
        {
            JArray array = JArray.Parse(JsonHelpers.GetJsonString(productsJsonFilePath));
            JObject itemToAdd = (JObject)JToken.FromObject(product);

            array.Add(itemToAdd);

            string jsonOutput = JsonConvert.SerializeObject(array, Formatting.Indented);
            File.WriteAllText(productsJsonFilePath, jsonOutput);
        }
        private bool MatchingProductExists(ProductModel product, List<ProductModel> existingProducts)
        {
            //if matching product found, return true
            return GetMatchingProduct(product.Name, existingProducts) != null;
        }
        private bool IdenticalProductExists(ProductModel updatedProduct, List<ProductModel> existingProducts)
        {
            bool result = existingProducts.Any(product => product.Name.Trim().ToLower() == updatedProduct.Name.Trim().ToLower() &&
                                                product.Barcode.Trim().ToLower() == updatedProduct.Barcode.Trim().ToLower() &&
                                                product.Price == updatedProduct.Price &&
                                                product.SelectedCategory.Category.Trim().ToLower() == updatedProduct.SelectedCategory.Category.Trim().ToLower());
            return result;
        }

        public ProductModel ValidateDataEntry(ProductModel product)
        {
            char systemDefaultDecimalSeparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            decimal validatedPrice = 0;

            if (product == null)
            {
                throw new ArgumentException("Suveskite visus prekės duomenis.");
            }

            if (product.SelectedCategory == null || product.SelectedCategory.Category.Contains("-"))
            {
                throw new ArgumentException("Pasirinkite kategoriją.");
            }
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Įveskite pavadinimą.");
            }
            #region numbers validation
            //validate amount (must be UINT) ---- give user informative message

            if (string.IsNullOrWhiteSpace(product.AmountInBox))
            {
                throw new ArgumentException("Įveskite kiekį.");
            }

            if (!uint.TryParse(product.AmountInBox, out uint temp))
            {
                throw new ArgumentException("Pataisykite kiekį. Tai turi būti teigiamas sveikasis skaičius (be kablelių).");
            }
            else if(temp ==0)
            {
                throw new ArgumentException("Įveskite kiekį.");
            }


            if (string.IsNullOrWhiteSpace(product.Price))
            {
                throw new ArgumentException("Įveskite kainą.");
            }
            try
            {
                validatedPrice = Convert.ToDecimal(product.Price.Replace('.', systemDefaultDecimalSeparator).Replace(',', systemDefaultDecimalSeparator));
                product.Price = validatedPrice.ToString("0.00");
            }
            catch (FormatException)
            {
                throw new ArgumentException("Pataisykite kainą. Naudokite tik vieną kablelį ir veskite tik skaičius.");
            }
            if (Convert.ToDecimal(product.Price) ==0)
            {
                throw new ArgumentException("Įveskite kainą.");
            }
            #endregion
            return product;
        }
    }
}
