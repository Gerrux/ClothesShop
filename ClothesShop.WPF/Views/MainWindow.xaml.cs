using System;
using System.Windows;
using ClothesShop.WPF.ViewModels;

namespace ClothesShop.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
        private void ClothesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("pack://application:,,,/Views/Pages/ClothesPage.xaml"));
        }
        private void ClothesTypesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("pack://application:,,,/Views/Pages/ClothesTypesPage.xaml"));
        }
        private void ManufacturersButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("pack://application:,,,/Views/Pages/ManufacturersPage.xaml"));
        }

        private void SuppliersButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("pack://application:,,,/Views/Pages/SuppliersPage.xaml"));
        }
        private void WarehousesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("pack://application:,,,/Views/Pages/WarehousesPage.xaml"));
        }
        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("pack://application:,,,/Views/Pages/OrdersPage.xaml"));
        }

    }
}
