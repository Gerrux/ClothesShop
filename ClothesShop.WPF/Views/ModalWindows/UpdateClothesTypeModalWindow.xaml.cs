using ClothesShop.Domain.Models;
using ClothesShop.WPF.ViewModels.ModalWindowViewModels;
using System;
using System.Windows;

namespace ClothesShop.WPF.Views.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для UpdateClothesTypeModalWindow.xaml
    /// </summary>
    public partial class UpdateClothesTypeModalWindow : Window
    {
        private ClothesType clothesType;

        public UpdateClothesTypeModalWindow(ClothesType clothesType)
        {
            InitializeComponent();
            UpdateClothesTypeModalWindowViewModel vm = new UpdateClothesTypeModalWindowViewModel(this.clothesType);
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }
    }
}
