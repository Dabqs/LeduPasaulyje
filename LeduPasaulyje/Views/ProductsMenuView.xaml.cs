﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public ProductsMenuView()
        {
            InitializeComponent();
        }

        // TODO : implement more of these for the convenience of the user
        private void SelectedProduct_AmountInBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.Tab))
                ((TextBox)sender).SelectAll();
        }
    }
}
