using ClothesShop.Domain.Models;
using ClothesShop.WPF.ViewModels.ModalWindowViewModels;
using System;
using System.Windows;

namespace ClothesShop.WPF.Views.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для UpdateManufacturerModalWindow.xaml
    /// </summary>
    public partial class UpdateManufacturerModalWindow : Window
    {
        private Manufacturer manufacturer;

        public UpdateManufacturerModalWindow(Manufacturer manufacturer)
        {
            InitializeComponent();
            UpdateManufacturerModalWindowViewModel vm = new UpdateManufacturerModalWindowViewModel(manufacturer);
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }
    }
}
