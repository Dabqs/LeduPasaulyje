using Caliburn.Micro;
using LeduPasaulyje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeduPasaulyje.ViewModels
{
   public class ShellViewModel : Conductor<object>
    {
        private InvoiceViewModel invoiceViewModel;
        private ProductsMenuViewModel productsMenuViewModel;
        private StoresMenuViewModel storesMenuViewModel;
        public int MyProperty { get; set; }

        public ShellViewModel(InvoiceViewModel invoiceViewModel, ProductsMenuViewModel productsMenuViewModel, StoresMenuViewModel storesMenuViewModel)
        {
            this.invoiceViewModel = invoiceViewModel;
            this.productsMenuViewModel = productsMenuViewModel;
            this.storesMenuViewModel = storesMenuViewModel;
            ActivateItem(this.invoiceViewModel);
        }
        public void LoadInvoiceMenu()
        {
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
