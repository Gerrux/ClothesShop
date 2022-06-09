using ClothesShop.WPF.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ClothesShop.WPF.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для UpdateClothesPage.xaml
    /// </summary>
    public partial class UpdateClothesPage : Page
    {
        public UpdateClothesPage(Guid clothesId, NavigationService mainFrame)
        {
            InitializeComponent();
            DataContext = new UpdateClothesPageViewModel(clothesId, mainFrame);
        }
    }
}
