using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using GRRX.ComponentModels;
using System;
using System.Windows.Input;

namespace ClothesShop.WPF.ViewModels.ModalWindowViewModels
{
    internal class UpdateSupplierModalWindowViewModel : ObservableObject
    {
        public UpdateSupplierModalWindowViewModel(Supplier supplier)
        {
            UpdatedSupplier = supplier;
            SupplierName = supplier.Name;
            UpdateCommand = new DelegateCommand(c =>
            {
                using DatabaseContext context = new();
                UpdatedSupplier.Name = SupplierName;
                context.Suppliers.Update(UpdatedSupplier);
                context.SaveChanges();
                CloseAction();
            });
        }

        public ICommand UpdateCommand { get; set; }
        public string SupplierName { get; set; }
        public Action CloseAction { get; set; }
        public Supplier UpdatedSupplier { get; set; }

    }
}
