using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using GRRX.ComponentModels;
using System;
using System.Windows.Input;

namespace ClothesShop.WPF.ViewModels.ModalWindowViewModels
{
    internal class UpdateWarehouseModalWindowViewModel : ObservableObject
    {
        public UpdateWarehouseModalWindowViewModel(Warehouse warehouse)
        {
            UpdatedWarehouse = warehouse;
            WarehouseName = warehouse.Name;
            UpdateCommand = new DelegateCommand(c =>
            {
                using DatabaseContext context = new();
                UpdatedWarehouse.Name = WarehouseName;
                context.Warehouses.Update(UpdatedWarehouse);
                context.SaveChanges();
                CloseAction();
            });
        }

        public ICommand UpdateCommand { get; set; }
        public string WarehouseName { get; set; }
        public Action CloseAction { get; set; }
        public Warehouse UpdatedWarehouse { get; set; }

    }
}
