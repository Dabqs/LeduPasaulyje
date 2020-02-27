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
    public class InvoiceViewModel : Screen
    {
        public BindableCollection<ProductModel> Products { get; set; }
        private ProductDataAccess productDataAccess = new ProductDataAccess();
        private ProductModel selectedProduct;
        public string Selected { get
            {
                if (SelectedProduct != null)
                {
                return $"{SelectedProduct.Name}";
                }
                else
                {
                    return string.Empty;
                }
            } }




        public ProductModel SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);

            }
        }


        public InvoiceViewModel()
        {
            Products = new BindableCollection<ProductModel>(productDataAccess.GetAllProducts());
        }

    }
}
