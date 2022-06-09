using ClothesShop.WPF.ViewModels;
using System.Windows.Controls;

namespace ClothesShop.WPF.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            DataContext = new OrdersPageViewModel();
        }
    }
}
