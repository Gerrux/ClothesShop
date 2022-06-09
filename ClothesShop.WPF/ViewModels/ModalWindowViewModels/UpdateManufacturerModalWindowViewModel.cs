using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using GRRX.ComponentModels;
using System;
using System.Windows.Input;

namespace ClothesShop.WPF.ViewModels.ModalWindowViewModels
{
    internal class UpdateManufacturerModalWindowViewModel : ObservableObject
    {
        public UpdateManufacturerModalWindowViewModel(Manufacturer manufacturer)
        {
            UpdatedManufacturer = manufacturer;
            ManufacturerName = manufacturer.Name;
            UpdateCommand = new DelegateCommand(c =>
            {
                using DatabaseContext context = new();
                UpdatedManufacturer.Name = ManufacturerName;
                context.Manufacturers.Update(UpdatedManufacturer);
                context.SaveChanges();
                CloseAction();
            });
        }

        public ICommand UpdateCommand { get; set; }
        public string ManufacturerName { get; set; }
        public Action CloseAction { get; set; }
        public Manufacturer UpdatedManufacturer { get; set; }

    }
}
