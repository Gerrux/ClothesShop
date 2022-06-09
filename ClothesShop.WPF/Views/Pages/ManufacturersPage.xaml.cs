using ClothesShop.WPF.ViewModels;
using System.Windows.Controls;

namespace ClothesShop.WPF.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManufacturersPage.xaml
    /// </summary>
    public partial class ManufacturersPage : Page
    {
        public ManufacturersPage()
        {
            InitializeComponent();
            this.DataContext = new ManufacturersPageViewModel();
        }
    }
}
