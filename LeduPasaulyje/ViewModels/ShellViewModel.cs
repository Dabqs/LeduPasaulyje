using Caliburn.Micro;
using LeduPasaulyje.Models;
using LeduPasaulyjeData.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeduPasaulyje.ViewModels
{
   public class ShellViewModel : Conductor<object>
    {
        //public BindableCollection<ProductModel> Products { get; set; }
        private InvoiceViewModel invoiceViewModel;
        private ProductsMenuViewModel productsMenuViewModel;
        private StoresMenuViewModel storesMenuViewModel;
        private ProductDataAccess productDataAccess = new ProductDataAccess();

        public ShellViewModel(InvoiceViewModel invoiceViewModel, ProductsMenuViewModel productsMenuViewModel, StoresMenuViewModel storesMenuViewModel)
        {

            this.invoiceViewModel = invoiceViewModel;
            this.productsMenuViewModel = productsMenuViewModel;
            this.storesMenuViewModel = storesMenuViewModel;
            ActivateItem(this.invoiceViewModel);
        }
        public void LoadInvoiceMenu()
        {
            invoiceViewModel.BuidIceCreamList();
            invoiceViewModel.BuidIceCreamList();
            ActivateItem(invoiceViewModel);
        }
        public void LoadStoresMenu()
        {
            ActivateItem(storesMenuViewModel);
        }
        public void LoadProductsMenu()
        {
            ActivateItem(productsMenuViewModel);
        }
    }
}
