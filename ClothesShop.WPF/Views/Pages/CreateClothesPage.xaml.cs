using ClothesShop.WPF.ViewModels;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ClothesShop.WPF.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateClothesPage.xaml
    /// </summary>
    public partial class CreateClothesPage : Page
    {
        public CreateClothesPage(NavigationService mainFrame)
        {
            InitializeComponent();
            DataContext = new CreateClothesPageViewModel(mainFrame);
        }
    }
}
