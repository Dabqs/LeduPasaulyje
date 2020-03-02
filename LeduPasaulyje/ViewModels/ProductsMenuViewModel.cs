using Caliburn.Micro;
using LeduPasaulyje.Commands;
using LeduPasaulyje.Models;
using LeduPasaulyjeData.Library;
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
        }

        private void GetProducts()
        {
            Products = new BindableCollection<ProductModel>();

            try
            {
                foreach (ProductModel item in productDataAccess.GetAllProducts().OrderBy(p => p.Name))
                {
                    products.Add(item);
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Nepavyko užkrauti produktų sąrašo. " + ex.Message);
            }
        }

        public void UpdateProducts()
        {
            ProductModel updatedProduct;
            SelectedProduct.SelectedCategory = SelectedCategorien;

            try
            {
                updatedProduct = productDataAccess.ValidateDataEntry(SelectedProduct);

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Įvyko netikėta klaida. Klaidos tekstas:'{ex.Message}'");
                return;
            }
            productDataAccess.UpdateProductsList(updatedProduct);
            GetProducts();
            CleanAllFields();
            MessageBox.Show($"Išsaugota sėkmingai!\n\n" +
                $"Kategorija: {updatedProduct.SelectedCategory.Category}\n" +
                $"Pavadinimas: {updatedProduct.Name}\n" +
                $"Kaina: {updatedProduct.Price}\n" +
                $"Kiekis {updatedProduct.AmountInBox}\n" +
                $"Prekės kodas: {updatedProduct.Barcode}");
        }

        public void CleanAllFields()
        {
            BindableCollection<ProductModel> tempProducts = Products;
            selectedProduct = null;
            Products = null;
            Products = tempProducts;
            SelectedProduct = new ProductModel(string.Empty, new CategoryModel() { Category = "--------" }, string.Empty, string.Empty, string.Empty);
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
