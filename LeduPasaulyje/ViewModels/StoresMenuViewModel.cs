﻿using Caliburn.Micro;
using LeduPasaulyje.Commands;
using LeduPasaulyjeData.Library;
using LeduPasaulyjeData.Library.Enums;
using LeduPasaulyjeData.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeduPasaulyje.ViewModels
{
    public class StoresMenuViewModel : Screen
    {
        private BindableCollection<string> allRegions;

        public BindableCollection<string> AllRegions
        {
            get { return allRegions; }
            set
            {
                allRegions = value;
                NotifyOfPropertyChange(() => AllRegions);
            }
        }
        private string selectedRegionName;

        public string SelectedRegionName
        {
            get { return selectedRegionName; }
            set
            {
                selectedRegionName = value;
                NotifyOfPropertyChange(() => SelectedRegionName);
                NotifyOfPropertyChange(() => CanAddToSelectedRegions);
            }
        }


        private BindableCollection<RegionModel> selectedRegions;

        public BindableCollection<RegionModel> SelectedRegions
        {
            get { return selectedRegions; }
            set
            {
                selectedRegions = value;
                NotifyOfPropertyChange(() => SelectedRegions);
            }
        }

        public void AddToSelectedRegions()
        {

            if (!string.IsNullOrWhiteSpace(SelectedRegionName))
            {
                BuildExistingRegions();

            }
            // if (SelectedRegions== null)
            // {
            //     SelectedRegions = new BindableCollection<RegionModel>();
            //     SelectedRegions.Add(new RegionModel(SelectedRegionName, true));
            // }

            // if (SelectedRegions.Any(r => r.Region != SelectedRegionName))
            // {
            SelectedRegions.Add(new RegionModel(SelectedRegionName, true));
            ReloadSelectedStore();
            NotifyOfPropertyChange(() => CanAddToSelectedRegions);
            // }

        }

        public bool CanAddToSelectedRegions
        {
            get
            {
                bool output = false;
                //check if not all regions are already sent there
                //if (SelectedRegions == null || (!string.IsNullOrWhiteSpace(SelectedRegionName) && !SelectedRegions.Any(r=> r.Region == SelectedRegionName)))
                if (SelectedRegions == null || (!string.IsNullOrWhiteSpace(SelectedRegionName) && !SelectedRegions.Any(r => r.Region == SelectedRegionName)))
                {
                    output = true;

                }
                return output;
            }
        }


        public void RemoveFromSelectedRegions()
        {

        }
        public bool CanRemoveFromSelectedRegions
        {
            get
            {
                bool output = false;
                //check if there are any items
                return output;
            }
        }



        private StoreModel selectedStore;
        public StoreModel SelectedStore
        {
            get { return selectedStore; }
            set
            {
                selectedStore = value;
                NotifyOfPropertyChange(() => SelectedStore);
                BuildExistingRegions();
            }
        }

        private BindableCollection<StoreModel> stores;
        private StoresDataAccess storesDataAccess = new StoresDataAccess();
        public BindableCollection<StoreModel> Stores
        {
            get { return stores; }
            set
            {
                stores = value;
                NotifyOfPropertyChange(() => Stores);
            }
        }

        public StoresMenuViewModel()
        {
            NullifySelectedStore = new NullifyObjectCommand(CleanAllFields);
            BuildAllRegions();
            //SelectedReg.Add(new KeyValuePair<string, bool>( "Utena", false));
            // storesDataAccess.AddStore(new StoreModel("Darola", "5564654", "LT-465464","Girulių g 5, LT-154487, Rokiškis", "Rokiškis"));
            // SelectedStore = new StoreModel(string.Empty, string.Empty, string.Empty, string.Empty);
            GetStores();
            //SelectedStore = Stores.First();
            SelectedStore = null;
            SelectedStore = new StoreModel(string.Empty, string.Empty, string.Empty, string.Empty, new List<RegionModel>());
        }
        private void GetStores()
        {
            Stores = new BindableCollection<StoreModel>();
            foreach (StoreModel store in storesDataAccess.GetAllStores().OrderBy(st => st.Name))
            {
                Stores.Add(store);
            }
        }

        public void BuildExistingRegions()
        {
            if (SelectedStore != null)
            {
                List<RegionModel> existingSelectedRegions = new List<RegionModel>();
                SelectedRegions = new BindableCollection<RegionModel>();
                existingSelectedRegions = SelectedStore.Regions.Where(r => r.IsSelected).ToList();

                foreach (RegionModel existingRegion in existingSelectedRegions)
                {
                    SelectedRegions.Add(existingRegion);
                }

            }
        }
        public void ReloadSelectedStore()
        {
            List<RegionModel> updatedRegions = new List<RegionModel>();
            foreach (string regionName in AllRegions)
            {
                bool isSelected = false;
                if (SelectedRegions.Any(r => r.Region == regionName))
                {
                    isSelected = true;
                }
                updatedRegions.Add(new RegionModel(regionName, isSelected));
            }
            SelectedStore = new StoreModel(SelectedStore.Name, SelectedStore.CompanyNumber, SelectedStore.VatNumber, SelectedStore.Address, updatedRegions);
        }
        public void BuildAllRegions()
        {
            AllRegions = new BindableCollection<string>();
            foreach (var item in Enum.GetValues(typeof(AvailableRegions)))
            {
                AllRegions.Add(item.ToString());
            }
        }

        public NullifyObjectCommand NullifySelectedStore { get; private set; }

        public void CleanAllFields()
        {
            BindableCollection<StoreModel> tempStores = Stores;
            SelectedStore = null;
            Stores = null;
            Stores = tempStores;
            SelectedStore = new StoreModel(string.Empty, string.Empty, string.Empty, string.Empty, new List<RegionModel>());
        }

        public void UpdateStores()
        {
            StoreModel updatedStore = selectedStore;
            selectedStore.Regions = new List<RegionModel>();

            foreach (string regionName in AllRegions)
            {
                bool isSelected = false;
                if (SelectedRegions.Any(r => r.Region == regionName))
                {
                    isSelected = true;
                }
                SelectedStore.Regions.Add(new RegionModel(regionName, isSelected));
            }

            //try
            //{
            //    updatedStore = storesDataAccess.ValidateDataEntry(SelectedProduct);
            //
            //}
            //catch (ArgumentException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Įvyko netikėta klaida. Klaidos tekstas:'{ex.Message}'");
            //    return;
            //}
            storesDataAccess.UpdateStoresList(updatedStore);
            GetStores();
            CleanAllFields();
            

           MessageBox.Show($"Išsaugota sėkmingai!\n\n" +
               $"Pavadinimas: {updatedStore.Name}\n" +
               $"Rajonai:\n    - {String.Join("\n    - ", updatedStore.Regions.Where(r=> r.IsSelected).Select(r => r.Region))}\n" +
               $"Įmonės kodas: {updatedStore.CompanyNumber}\n" +
               $"PVM kodas {updatedStore.VatNumber}\n" +
               $"Adresas: {updatedStore.Address}");
        }


        public void DeleteStore()
        {
            if (MessageBox.Show("Ar tikrai norite panaikinti šią parduotuvę? Ji bus ištrinta iš sąrašo visiems laikams.", "DĖMESIO!", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (SelectedStore != null)
                {
                    storesDataAccess.RemoveStore(SelectedStore);
                    GetStores();
                    CleanAllFields();
                }
            }
        }
    }
}
