using Caliburn.Micro;
using LeduPasaulyje.Commands;
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
                if (value != null)
                {
                SelectedCategorien = value.SelectedCategory;
                }
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
            NullifySelectedProduct = new NullifyObjectCommand(CleanAllFields);
            GetProducts();
            CleanAllFields();
            //selectedProduct = new ProductModel(0, new CategoryModel() { Category = "Šaldyti produktai" }, "Pasirinkite", 0, 0);

        }

        private void GetProducts()
        {
            Products = new BindableCollection<ProductModel>();
            foreach (ProductModel item in productDataAccess.GetAllProducts())
            {
                products.Add(item);
            }
        }

        public void UpdateProducts()
        {
            ProductModel product = SelectedProduct;
            SelectedProduct.SelectedCategory = SelectedCategorien;
            try
            {
            productDataAccess.ValidateDataEntry(SelectedProduct);

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Įvyko netikėta klaida. Klaidos tekstas:'{ex.Message}'");
                return;
            }
            productDataAccess.UpdateProductsList(product);
            GetProducts();
            SelectedProduct = product;
            MessageBox.Show("Išsaugota sėkmingai");
        }

        public void CleanAllFields()
        {
            BindableCollection<ProductModel> tempProducts = Products;
            selectedProduct = null;
            Products = null;
            Products = tempProducts;
            SelectedProduct = new ProductModel(0, new CategoryModel() {Category = "--------" }, string.Empty, 0, 0);
        }
        public NullifyObjectCommand NullifySelectedProduct { get; private set; }

        public void DeleteProduct()
        {
            if (MessageBox.Show("Ar tikrai norite panaikinti šį produktą? Jis bus ištrintas iš sąrašo visiems laikams.", "DĖMESIO!", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (SelectedProduct != null)
                {
                    productDataAccess.RemoveProduct(SelectedProduct);
                    GetProducts();
                    CleanAllFields();
                }
            }
        }
    }
}
