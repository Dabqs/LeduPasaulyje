using LeduPasaulyjeData.Library.Helpers;
using LeduPasaulyjeData.Library.Models;
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
    public class StoresDataAccess
    {
       private readonly string storesJsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Stores.json");

        public List<StoreModel> GetAllStores()
        {
            return JsonConvert.DeserializeObject<List<StoreModel>>(JsonHelpers.GetJsonString(storesJsonFilePath));
        }

        public void UpdateStoresList(StoreModel store)
        {
            List<StoreModel> existingStores = GetAllStores();

            if (!IdenticalStoreExists(store, existingStores))
            {
                if (MatchingStoreExists(store, existingStores))
                {//Edit existing store
                    EditStore(store, existingStores);
                }
                else
                {
                    AddStore(store);
                }
            }
        }
        public void AddStore(StoreModel store)
        {
            JArray array = JArray.Parse(JsonHelpers.GetJsonString(storesJsonFilePath));
            JObject itemToAdd = (JObject)JToken.FromObject(store);

            array.Add(itemToAdd);

            string jsonOutput = JsonConvert.SerializeObject(array, Formatting.Indented);
            File.WriteAllText(storesJsonFilePath, jsonOutput);
        }

        private bool IdenticalStoreExists(StoreModel updatedStore, List<StoreModel> existingStores)
        {
            bool result = existingStores.Any(store => store.Name == updatedStore.Name &&
                                                store.CompanyNumber.Trim().ToLower() == updatedStore.CompanyNumber.Trim().ToLower() &&
                                                store.VatNumber.Trim().ToLower() == updatedStore.VatNumber.Trim().ToLower() &&
                                                store.SelectedRegion.Region.Trim().ToLower() == updatedStore.SelectedRegion.Region.Trim().ToLower());
            return result;
        }

        private bool MatchingStoreExists(StoreModel store, List<StoreModel> existingStores)
        {
            //if matching product found, return true
            return GetMatchingStore(store.Name, existingStores) != null;
        }
        private StoreModel GetMatchingStore(string name, List<StoreModel> existingStores)
        {
            return existingStores.Where(store => store.Name == name).FirstOrDefault();
        }
        private void EditStore(StoreModel store, List<StoreModel> existingStores)
        {
            int productIndex = existingStores.IndexOf(GetMatchingStore(store.Name, existingStores));
            dynamic jsonObj = JsonConvert.DeserializeObject(JsonHelpers.GetJsonString(storesJsonFilePath));
            jsonObj[productIndex]["CompanyNumber"] = store.CompanyNumber;
            jsonObj[productIndex]["SelectedRegion"]["Region"] = store.SelectedRegion.Region; // TODO : create list of selected regions?
            jsonObj[productIndex]["VatNumber"] = store.VatNumber;

            string jsonOutput = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(storesJsonFilePath, jsonOutput);

        }
        // TODO : 
        //Implement!!!!
      //  public void RemoveStore(StoreModel store)
      //  {
      //      List<StoreModel> existingStores = GetAllStores();
      //      if (IdenticalStoreExists(store, existingStores))
      //      {
      //          List<StoreModel> updatedProducts = existingStores.Where(p => p.Name != store.Name || p.SelectedCategory.Category != store.SelectedCategory.Category).ToList();
      //          string jsonOutput = JsonConvert.SerializeObject(updatedProducts, Formatting.Indented);
      //          File.WriteAllText(productsJsonFilePath, jsonOutput);
      //      }
      //  }
    }
}
