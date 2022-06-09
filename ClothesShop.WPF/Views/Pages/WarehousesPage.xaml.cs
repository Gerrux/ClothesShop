using ClothesShop.WPF.ViewModels;
using System.Windows.Controls;

namespace ClothesShop.WPF.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для WarehousesPage.xaml
    /// </summary>
    public partial class WarehousesPage : Page
    {
        public WarehousesPage()
        {
            InitializeComponent();
            DataContext = new WarehousesPageViewModel();
        }
    }
}
