using ClothesShop.WPF.ViewModels;
using System.Windows.Controls;

namespace ClothesShop.WPF.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClothesTypesPage.xaml
    /// </summary>
    public partial class ClothesTypesPage : Page
    {
        public ClothesTypesPage()
        {
            InitializeComponent();
            this.DataContext = new ClothesTypesPageViewModel();
        }
    }
}
