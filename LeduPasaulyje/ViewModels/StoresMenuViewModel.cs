using Caliburn.Micro;
using LeduPasaulyjeData.Library;
using LeduPasaulyjeData.Library.Enums;
using LeduPasaulyjeData.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (SelectedRegions.Any(r => r.Region != SelectedRegionName))
            {
            SelectedRegions.Add(new RegionModel(SelectedRegionName,true));
            ReloadSelectedStore();
                NotifyOfPropertyChange(() => CanAddToSelectedRegions);
            }

        }

        public bool CanAddToSelectedRegions
        {
            get
            {
                bool output = false;
                //check if not all regions are already sent there
                if (!string.IsNullOrWhiteSpace(SelectedRegionName) && !SelectedRegions.Any(r=> r.Region == SelectedRegionName))
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
            BuildAllRegions();
            //SelectedReg.Add(new KeyValuePair<string, bool>( "Utena", false));
            // storesDataAccess.AddStore(new StoreModel("Darola", "5564654", "LT-465464","Girulių g 5, LT-154487, Rokiškis", "Rokiškis"));
            // SelectedStore = new StoreModel(string.Empty, string.Empty, string.Empty, string.Empty);
            GetStores();
            SelectedStore = Stores.First();
        }
        private void GetStores()
        {
            Stores = new BindableCollection<StoreModel>();
            foreach (StoreModel store in storesDataAccess.GetAllStores())
            {
                Stores.Add(store);
            }
        }

        public void BuildExistingRegions()
        {
            List<RegionModel> existingSelectedRegions = new List<RegionModel>();
            SelectedRegions = new BindableCollection<RegionModel>();
            existingSelectedRegions = SelectedStore.Regions.Where(r => r.IsSelected).ToList();

            foreach (RegionModel existingRegion in existingSelectedRegions)
            {
                SelectedRegions.Add(existingRegion);
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
    }
}
