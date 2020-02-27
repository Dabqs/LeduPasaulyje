using LeduPasaulyje.Models;
using LeduPasaulyjeData.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeduPasaulyje.Views
{
    /// <summary>
    /// Interaction logic for ProductsMenuView.xaml
    /// </summary>
    public partial class ProductsMenuView : UserControl
    {
        List<string> possibleCategories = new List<string>()
            {
               // string.Empty,
               "Ledai",
                "Šaldyti produktai"
            };
        public ProductsMenuView()
        {
            InitializeComponent();
            CategorySelection.ItemsSource = possibleCategories;
        }

        private void BtnAtsauktiPasirinkima_Click(object sender, RoutedEventArgs e)
        {
            ProductsList.SelectedItem = null;

        }

        private void ProductsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ProductModel selectedProduct = (ProductModel)ProductsList.SelectedItem;
            if (selectedProduct != null)
            {
                var selectedProductCategory = selectedProduct.Category;
                switch (selectedProductCategory)
                {
                    case "Ledai":
                        CategorySelection.SelectedItem = possibleCategories[0];
                        break;
                    case "Šaldyti produktai":
                        CategorySelection.SelectedItem = possibleCategories[1];
                        break;
                    default:
                        break;
                }
            }
            else
            {
                CategorySelection.SelectedItem = string.Empty;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ar tikrai norite panaikinti šį produktą? Jis bus ištrintas iš sąrašo visiems laikams.", "DĖMESIO!", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ProductDataAccess dataAccess = new ProductDataAccess();
                ProductModel selectedProduct = (ProductModel)ProductsList.SelectedItem;
                if (selectedProduct != null)
                {
                    dataAccess.RemoveProduct(selectedProduct);
                    ProductsList.ItemsSource = dataAccess.GetAllProducts();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ProductsList.SelectedItem = null;
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateDataEntry();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            ProductDataAccess dataAccess = new ProductDataAccess();
            dataAccess.UpdateProductsList(BuildProductModel());
            ProductsList.ItemsSource = dataAccess.GetAllProducts();
            MessageBox.Show("Produktas išsaugotas sėkmingai");
        }
        private ProductModel BuildProductModel()
        {
            string name = SelectedProduct_Name.Text;
            string barcode = SelectedProduct_Barcode.Text;
            decimal price = Convert.ToDecimal(SelectedProduct_Price.Text);
            uint amount = Convert.ToUInt32(SelectedProduct_AmountInBox.Text);
            string category = CategorySelection.Text;

            return new ProductModel(barcode, category, name, amount, price);
        }
        private void ValidateDataEntry()
        {
            char systemDefaultDecimalSeparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            decimal validatedPrice = 0;
            if (CategorySelection.SelectedItem == null)
            {
                throw new ArgumentException("Pasirinkite kategoriją.");
            }
            if (string.IsNullOrWhiteSpace(SelectedProduct_Name.Text))
            {
                throw new ArgumentException("Įveskite pavadinimą.");
            }
            if (string.IsNullOrWhiteSpace(SelectedProduct_AmountInBox.Text))
            {
                throw new ArgumentException("Įveskite kiekį.");
            }
            if (!uint.TryParse(SelectedProduct_AmountInBox.Text, out uint temp))
            {
                throw new ArgumentException("Pataisykite kiekį. Tai turi būti teigiamas sveikasis skaičius (be kablelių).");
            }
            if (string.IsNullOrWhiteSpace(SelectedProduct_Price.Text))
            {
                throw new ArgumentException("Įveskite kainą.");
            }
            try
            {
                validatedPrice = Convert.ToDecimal(SelectedProduct_Price.Text.Replace('.', systemDefaultDecimalSeparator).Replace(',', systemDefaultDecimalSeparator));
                SelectedProduct_Price.Text = validatedPrice.ToString("0.00");
            }
            catch (Exception)
            {
                throw new Exception("Pataisykite kainą. Naudokite tik vieną kablelį ir veskite tik skaičius.");
            }
        }


    }
}
