using LeduPasaulyjeData.Library.Helpers;
using LeduPasaulyjeData.Library.Models;
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

                if (MatchingStoreExists(store, existingStores))
                {//Edit existing store
                    EditStore(store, existingStores);
                }
                else
                {
                    AddStore(store);
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
        private bool MatchingStoreExists(StoreModel store, List<StoreModel> existingStores)
        {
            //if matching store found, return true
            return GetMatchingStore(store.Name, existingStores) != null;
        }
        private StoreModel GetMatchingStore(string name, List<StoreModel> existingStores)
        {
            return existingStores.Where(store => store.Name == name).FirstOrDefault();
        }
        private void EditStore(StoreModel store, List<StoreModel> existingStores)
        {
            int productIndex = existingStores.IndexOf(GetMatchingStore(store.Name, existingStores));
            JArray array = JArray.Parse(JsonHelpers.GetJsonString(storesJsonFilePath));
            array[productIndex].Remove();
            JObject itemToAdd = (JObject)JToken.FromObject(store);
            array.Add(itemToAdd);

            string jsonOutput = JsonConvert.SerializeObject(array, Formatting.Indented);
            File.WriteAllText(storesJsonFilePath, jsonOutput);
        }

        public void RemoveStore(StoreModel store)
        {
            List<StoreModel> existingStores = GetAllStores();
            if (MatchingStoreExists(store, existingStores))
            {
                List<StoreModel> updatedStores = existingStores.Where(existingStore => existingStore.Name != store.Name).ToList();
                string jsonOutput = JsonConvert.SerializeObject(updatedStores, Formatting.Indented);
                File.WriteAllText(storesJsonFilePath, jsonOutput);
            }
        }

        public StoreModel ValidateDataEntry(StoreModel store)
        {
            // - Vat code is optional
            if (store == null)
            {
                throw new ArgumentException("Suveskite visus prekės duomenis.");
            }

            if (!store.Regions.Any(r => r.IsSelected))
            {
                throw new ArgumentException("Pasirinkite nors vieną rajoną.");
            }
            if (string.IsNullOrWhiteSpace(store.Name))
            {
                throw new ArgumentException("Įveskite pavadinimą.");
            }
            if (String.IsNullOrWhiteSpace(store.CompanyNumber))
            {
                throw new ArgumentException("Įveskite įmonės kodą.");
            }
            if (String.IsNullOrWhiteSpace(store.CompanyNumber))
            {
                throw new ArgumentException("Įveskite įmonės kodą.");
            }
            if (String.IsNullOrWhiteSpace(store.Address))
            {
                throw new ArgumentException("Įveskite adresą.");
            }
            return store;
        }
    }
}
