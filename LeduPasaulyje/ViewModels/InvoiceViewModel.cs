using Caliburn.Micro;
using LeduPasaulyje.Models;
using LeduPasaulyjeData.Library;
using LeduPasaulyjeData.Library.Enums;
using LeduPasaulyjeData.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeduPasaulyje.ViewModels
{
    public class InvoiceViewModel : Screen
    {
        private DateTime invoiceDate;
        public DateTime InvoiceDate
        {
            get { return invoiceDate; }
            set
            {
                invoiceDate = value;
                NotifyOfPropertyChange(() => InvoiceDate);
            }
        }

        private BindableCollection<StoreModel> allStores;

        public BindableCollection<StoreModel> AllStores
        {
            get { return allStores; }
            set
            {
                allStores = value;
                NotifyOfPropertyChange(() => AllStores);
            }
        }
        public void GetAllStores()
        {
            AllStores = new BindableCollection<StoreModel>();

            foreach (StoreModel store in storesDataAccess.GetAllStores())
            {
                AllStores.Add(store);
            }

        }

        private DataTable invoiceDt;
        public DataTable InvoiceDt
        {
            get
            {
                return invoiceDt;
            }
        }

        private BindableCollection<StoreModel> filteredStoresList;

        public BindableCollection<StoreModel> FilteredStoresList
        {
            get { return filteredStoresList; }
            set
            {
                filteredStoresList = GetFilteredStores();
                NotifyOfPropertyChange(() => FilteredStoresList);
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


        private InvoiceDataAccess invoiceDataAccess = new InvoiceDataAccess();
        private ProductDataAccess productDataAccess = new ProductDataAccess();
        private StoresDataAccess storesDataAccess = new StoresDataAccess();

        private string selectedRegionName;

        public string SelectedRegionName
        {
            get { return selectedRegionName; }
            set
            {
                selectedRegionName = value;
                FilteredStoresList = GetFilteredStores();
                NotifyOfPropertyChange(() => FilteredStoresList);
                NotifyOfPropertyChange(() => SelectedRegionName);
            }
        }

        private BindableCollection<StoreModel> GetFilteredStores()
        {
            BindableCollection<StoreModel> filteredBySelectedRegion = new BindableCollection<StoreModel>();

            foreach (StoreModel storeModel in AllStores)
            {
                if (storeModel.Regions.Any(s => s.Region == SelectedRegionName && s.IsSelected))
                {
                    filteredBySelectedRegion.Add(storeModel);
                }
            }
            return filteredBySelectedRegion;
        }


        private BindableCollection<ProductModel> iceCreamList;
        public BindableCollection<ProductModel> IceCreamList
        {
            get { return iceCreamList; }
            set
            {
                iceCreamList = value;
                NotifyOfPropertyChange(() => IceCreamList);
            }
        }
        public void BuidIceCreamList()
        {
            IceCreamList = new BindableCollection<ProductModel>();
            foreach (ProductModel product in productDataAccess.GetAllProducts().Where(p => p.SelectedCategory.Category == "Ledai"))
            {
                IceCreamList.Add(product);
            }
        }
        private BindableCollection<ProductModel> icedProductsList;

        public BindableCollection<ProductModel> IcedProductsList
        {
            get { return icedProductsList; }
            set
            {
                icedProductsList = value;
                NotifyOfPropertyChange(() => IcedProductsList);
            }
        }

        public void BuildIcedProductsList()
        {
            IcedProductsList = new BindableCollection<ProductModel>();
            foreach (ProductModel product in productDataAccess.GetAllProducts().Where(p => p.SelectedCategory.Category == "Šaldyti produktai"))
            {
                IcedProductsList.Add(product);
            }
        }


        private InvoiceModel invoiceNumberData;

        public InvoiceModel InvoiceNumberData
        {
            get { return invoiceNumberData; }
            set
            {
                invoiceNumberData = value;
                NotifyOfPropertyChange(() => InvoiceNumberData);
            }
        }

        public void Print()
        {
            if (SelectedStore == null)
            {
                MessageBox.Show("Pasirinkite parduotuvę.");
            }
            invoiceDataAccess.UpdateInvoiceData();
            GetInvoiceData();

        }

        public void CleanInvoiceFields()
        {
            ResetDate();
            InitDataTable();
            GetAllStores();
            BuildAllRegions();

        }

        public void ResetDate()
        {
            InvoiceDate = DateTime.Now;
        }

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
        public void BuildAllRegions()
        {
            AllRegions = new BindableCollection<string>();
            foreach (var item in Enum.GetValues(typeof(AvailableRegions)))
            {
                AllRegions.Add(item.ToString());
            }
        }
        private ProductModel selectedIcedProduct;

        public ProductModel SelectedIcedProduct
        {
            get { return selectedIcedProduct; }
            set
            {
                selectedIcedProduct = value;
                NotifyOfPropertyChange(() => SelectedIcedProduct);
                NotifyOfPropertyChange(() => CanAddIcedProduct);
            }
        }

        private DataRowView selectedDr;
        public DataRowView SelectedDr
        {
            get { return selectedDr; }
            set
            {
                selectedDr = value;
                NotifyOfPropertyChange(() => SelectedDr);
                NotifyOfPropertyChange(() => CanRemoveRow);
            }
        }

        public bool CanRemoveRow
        {
            get
            {
                bool output = false;
                if (SelectedDr != null)
                {
                    output = true;
                }
                return output;
            }
        }
        public void RemoveRow()
        {
            //implement
        }

        private ProductModel selectedIceCream;

        public ProductModel SelectedIceCream
        {
            get { return selectedIceCream; }
            set
            {
                selectedIceCream = value;
                NotifyOfPropertyChange(() => SelectedIceCream);
                NotifyOfPropertyChange(() => CanAddIceCream);
            }
        }

        public bool CanAddIceCream
        {
            get
            {
                bool output = false;
                if (SelectedIceCream != null)
                {
                    output = true;
                }
                return output;
            }
        }
        public void AddIceCream()
        {
            DataRow rowToAdd = invoiceDt.NewRow();
            rowToAdd["Barkodas"] = SelectedIceCream.Barcode;
            rowToAdd["Pavadinimas"] = SelectedIceCream.Name;
            rowToAdd["Dėžės"] = "1";
            rowToAdd["Kiekis"] = SelectedIceCream.AmountInBox;
            rowToAdd["Kaina"] = SelectedIceCream.Price;
            rowToAdd["Suma"] = $"{Convert.ToInt32(SelectedIceCream.AmountInBox) * Convert.ToDecimal(SelectedIceCream.Price)}";
            rowToAdd["Nuolaida"] = "0";

            InvoiceDt.Rows.Add(rowToAdd);
            BuidIceCreamList();
        }
        public bool CanAddIcedProduct
        {
            get
            {
                bool output = false;
                if (SelectedIcedProduct != null)
                {
                    output = true;
                }
                return output;
            }
        }
        public void AddIcedProduct()
        {
            DataRow rowToAdd = invoiceDt.NewRow();
            rowToAdd["Barkodas"] = SelectedIcedProduct.Barcode;
            rowToAdd["Pavadinimas"] = SelectedIcedProduct.Name;
            rowToAdd["Dėžės"] = "1";
            rowToAdd["Kiekis"] = SelectedIcedProduct.AmountInBox;
            rowToAdd["Kaina"] = SelectedIcedProduct.Price;
            rowToAdd["Suma"] = $"{Convert.ToInt32(SelectedIcedProduct.AmountInBox) * Convert.ToDecimal(SelectedIcedProduct.Price)}";
            rowToAdd["Nuolaida"] = "0";

            InvoiceDt.Rows.Add(rowToAdd);
            //BuidIceCreamList();
            BuildIcedProductsList();
        }

        public InvoiceViewModel()
        {

            InitDataTable();
            //selectedIceCream = new ProductModel(string.Empty, new CategoryModel() { Category = "----" }, string.Empty, string.Empty, string.Empty);
            ResetDate();
            BuildAllRegions();
            BuildIcedProductsList();
            BuidIceCreamList();
            GetInvoiceData();
            GetAllStores();
        }

        public void InitDataTable()
        {
            invoiceDt = new DataTable();
            invoiceDt.Clear();
            invoiceDt.Columns.Add("Barkodas");
            invoiceDt.Columns.Add("Pavadinimas");
            invoiceDt.Columns.Add("Dėžės");
            invoiceDt.Columns.Add("Kiekis");
            invoiceDt.Columns.Add("Kaina");
            invoiceDt.Columns.Add("Suma");
            invoiceDt.Columns.Add("Nuolaida");
            NotifyOfPropertyChange(()=> InvoiceDt);
        }

        private void GetInvoiceData()
        {
            try
            {
                InvoiceNumberData = invoiceDataAccess.GetInvoiceData();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Nepavyko aptikti paskutinio sąskaitos numerio. " + ex.Message);
            }
        }

    }
}
