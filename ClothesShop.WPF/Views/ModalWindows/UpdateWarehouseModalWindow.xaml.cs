using ClothesShop.Domain.Models;
using ClothesShop.WPF.ViewModels.ModalWindowViewModels;
using System;
using System.Windows;

namespace ClothesShop.WPF.Views.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для UpdateWarehouseModalWindow.xaml
    /// </summary>
    public partial class UpdateWarehouseModalWindow : Window
    {
        private Warehouse warehouse;

        public UpdateWarehouseModalWindow(Warehouse warehouse)
        {
            InitializeComponent();
            UpdateWarehouseModalWindowViewModel vm = new UpdateWarehouseModalWindowViewModel(warehouse);
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }
    }
}
