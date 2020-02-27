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
    public class ProductsMenuViewModel : Screen
    {
        ProductDataAccess dataAccess = new ProductDataAccess();
        public BindableCollection<ProductModel> Products { get; set; }
        private ProductModel selectedProduct;
       
   


        public ProductModel SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);

            }
        }


        public ProductsMenuViewModel()
        {
            Products = new BindableCollection<ProductModel>(dataAccess.GetAllProducts());
        }


    }
}
