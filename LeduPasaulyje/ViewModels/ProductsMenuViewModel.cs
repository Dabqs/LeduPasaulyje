using Caliburn.Micro;
using LeduPasaulyje.Models;
using LeduPasaulyjeData.Library;
using LeduPasaulyjeData.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LeduPasaulyje.ViewModels
{
    public class ProductsMenuViewModel : Screen
    {
        private ProductDataAccess productDataAccess = new ProductDataAccess();
        private CategoryModel selectedCategorien;
        public CategoryModel SelectedCategorien
        {
            get { return selectedCategorien; }
            set
            {
                selectedCategorien = value;
                // SelectedProduct.SelectedCategory = value;
                NotifyOfPropertyChange(() => SelectedCategorien);

            }
        }
        private ProductModel selectedProduct;
        public ProductModel SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                SelectedCategorien = value.SelectedCategory;
                NotifyOfPropertyChange(() => SelectedProduct);

            }
        }
        public BindableCollection<ProductModel> products;
        public BindableCollection<ProductModel> Products
        {
            get { return products; }
            set
            {
                products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }
        public ProductsMenuViewModel()
        {

            GetProducts();
            selectedProduct = new ProductModel(0, new CategoryModel() { Category = "Šaldyti produktai" }, "Pasirinkite", 0, 0);
        }

        private void GetProducts()
        {
            Products = new BindableCollection<ProductModel>();
            foreach (ProductModel item in productDataAccess.GetAllProducts())
            {
                products.Add(item);
            }
        }

        public void AddAProduct()
        {
            ProductModel product = new ProductModel(SelectedProduct.Barcode, SelectedCategorien, SelectedProduct.Name, SelectedProduct.AmountInBox, SelectedProduct.Price);
            productDataAccess.UpdateProductsList(product);
            GetProducts();
            selectedProduct = product;
            MessageBox.Show("Išsaugota sėkmingai");
        }

    }
}
