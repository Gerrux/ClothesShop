using ClothesShop.WPF.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ClothesShop.WPF.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClothesPage.xaml
    /// </summary>
    public partial class ClothesPage : Page
    {
        public ClothesPage()
        {
            InitializeComponent();
            this.DataContext = new ClothesPageViewModel();
        }

        private void CreateClothes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateClothesPage(NavigationService));
        }
        private void SortItems_Click(object sender, RoutedEventArgs e)
        {
            if (SortImage.Tag == "SortedDown")
            {
                SortImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/Source/Images/sort-up.png"));
                SortImage.Tag = "SortedUp";
            }
            else
            {
                SortImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/Source/Images/sort-down.png"));
                SortImage.Tag = "SortedDown";
            }

        }
        private void OpenClothes_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                NavigationService.Navigate(new PresentationClothesPage((Guid)((Border)sender).Tag, NavigationService));
        }
        public void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var binding = ((TextBox)sender).GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }

    }
}
