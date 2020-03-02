using Caliburn.Micro;
using LeduPasaulyje.Models;
using LeduPasaulyjeData.Library;
using LeduPasaulyjeData.Library.Enums;
using LeduPasaulyjeData.Library.Models;
using System;
using System.Collections.Generic;
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

        private InvoiceDataAccess invoiceDataAccess = new InvoiceDataAccess();
        private ProductDataAccess productDataAccess = new ProductDataAccess();
        private StoresDataAccess storesDataAccess = new StoresDataAccess();

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

        public void BuidIcedProductsList()
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
            invoiceDataAccess.UpdateInvoiceData();
            GetInvoiceData();

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

        public InvoiceViewModel()
        {
            ResetDate();
            BuildAllRegions();
            BuidIcedProductsList();
            BuidIceCreamList();
            GetInvoiceData();
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
