using ClothesShop.WPF.ViewModels;
using System.Windows.Controls;

namespace ClothesShop.WPF.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для SuppliersPage.xaml
    /// </summary>
    public partial class SuppliersPage : Page
    {
        public SuppliersPage()
        {
            InitializeComponent();
            DataContext = new SuppliersPageViewModel();
        }
    }
}
