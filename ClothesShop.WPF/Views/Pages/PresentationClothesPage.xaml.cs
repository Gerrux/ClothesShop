using ClothesShop.WPF.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ClothesShop.WPF.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для PresentationClothesPage.xaml
    /// </summary>
    public partial class PresentationClothesPage : Page
    {
        public PresentationClothesPage(Guid clothesId, NavigationService mainFrame)
        {
            InitializeComponent();
            DataContext = new PresentationClothesPageViewModel(clothesId, mainFrame);
        }

        private void OpenClothes_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                NavigationService.Navigate(new PresentationClothesPage((Guid)((Border)sender).Tag, NavigationService));
        }
    }
}
