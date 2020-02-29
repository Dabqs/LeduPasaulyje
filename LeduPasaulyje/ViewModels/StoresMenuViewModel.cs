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
        private List<string> selectedRegions;

        public List<string> SelectedRegions
        {
            get { return selectedRegions; }
            set { selectedRegions = value;
                NotifyOfPropertyChange(() => SelectedRegions);
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
            SelectedStore = new StoreModel(string.Empty, string.Empty, string.Empty, new RegionModel() { Region = "----" });
            GetStores();
        }
        private void GetStores()
        {
            Stores = new BindableCollection<StoreModel>();
            foreach (StoreModel store in storesDataAccess.GetAllStores())
            {
                Stores.Add(store);
            }
        }
    }
}
