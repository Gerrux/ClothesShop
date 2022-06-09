using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using GRRX.ComponentModels;
using System;
using System.Windows.Input;

namespace ClothesShop.WPF.ViewModels.ModalWindowViewModels
{
    internal class UpdateClothesTypeModalWindowViewModel : ObservableObject
    {
        public UpdateClothesTypeModalWindowViewModel(ClothesType clothesType)
        {
            UpdatedClothesType = clothesType;
            ClothesTypeName = clothesType.Name;
            UpdateCommand = new DelegateCommand(c =>
            {
                using DatabaseContext context = new();
                UpdatedClothesType.Name = ClothesTypeName;
                context.ClothesTypes.Update(UpdatedClothesType);
                context.SaveChanges();
                CloseAction();
            });
        }

        public ICommand UpdateCommand { get; set; }
        public string ClothesTypeName { get; set; }
        public Action CloseAction { get; set; }
        public ClothesType UpdatedClothesType { get; set; }

    }
}
