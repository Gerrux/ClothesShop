using System;
using System.Windows;
using ClothesShop.WPF.ViewModels.ModalWindowViewModels;

namespace ClothesShop.WPF.Views.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для CreateOrderModalWindow.xaml
    /// </summary>
    public partial class CreateOrderModalWindow : Window
    {
        public CreateOrderModalWindow(Guid clothesId)
        {
            InitializeComponent();
            CreateOrderModalWindowViewModel vm = new CreateOrderModalWindowViewModel(clothesId);
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }
    }
}
