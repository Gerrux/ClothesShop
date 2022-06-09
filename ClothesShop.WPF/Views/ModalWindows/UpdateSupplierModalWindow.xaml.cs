using ClothesShop.Domain.Models;
using ClothesShop.WPF.ViewModels.ModalWindowViewModels;
using System;
using System.Windows;

namespace ClothesShop.WPF.Views.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для UpdateSupplierModalWindow.xaml
    /// </summary>
    public partial class UpdateSupplierModalWindow : Window
    {
        private Supplier supplier;

        public UpdateSupplierModalWindow(Supplier supplier)
        {
            InitializeComponent();
            UpdateSupplierModalWindowViewModel vm = new UpdateSupplierModalWindowViewModel(this.supplier);
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }
    }
}
