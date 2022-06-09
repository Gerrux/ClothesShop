using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using ClothesShop.WPF.Views.ModalWindows;
using GRRX.ComponentModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ClothesShop.WPF.ViewModels
{
    internal class SuppliersPageViewModel : ObservableObject
    {
        public SuppliersPageViewModel(ObservableCollection<Supplier> suppliers)
        => Suppliers = suppliers ?? throw new ArgumentNullException(nameof(suppliers));

        private static bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new DependencyObject());
        public SuppliersPageViewModel()
                : this(new ObservableCollection<Supplier>())
        {
            InitCommands();
            if (!IsInDesignMode)
            {
                FillForm();
            }
        }
        public void InitCommands()
        {
            UpdateItemCommand = new DelegateCommand(UpdateItem);
            DeleteItemCommand = new DelegateCommand(DeleteItem);
            SaveNewItemCommand = new DelegateCommand(SaveNewItem);
        }
        public ICommand SaveNewItemCommand { get; set; }
        public ICommand UpdateItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }

        private void FillForm()
        {
            List<Supplier> list;
            using (var connect = new DatabaseContext())
                list = connect.Suppliers.ToList();

            list.ForEach(clt => Suppliers.Add(clt));
            MyItemsSource = list;
        }

        private void SaveNewItem(object obj)
        {
            if (NewItemName == null)
                MessageBox.Show("Пустое название!");
            else
            {
                DatabaseContext _context = new();
                Supplier supplier = new() { Name = NewItemName };
                _context.Suppliers.Add(supplier);
                _context.SaveChanges();
                MessageBox.Show($"Новый производитель: {NewItemName} успешно сохранён!");

                FillForm();
            }
        }

        private void UpdateItem(object parameter)
        {
            Guid idItem = (Guid)parameter;
            using DatabaseContext _context = new();
            Supplier supplier = _context.Suppliers.First(c => c.Id == idItem);
            UpdateSupplierModalWindow window = new(supplier);
            window.ShowDialog();
            FillForm();
        }
        private void DeleteItem(object parameter)
        {
            Guid idItem = (Guid)parameter;
            using DatabaseContext _context = new();
            Supplier supplier = _context.Suppliers.First(c => c.Id == idItem);
            MessageBox.Show($"Удалён поставщик: {supplier.Name}!");
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
            MyItemsSource = _context.Suppliers.ToList();
        }

        private string newItemName;
        public string NewItemName { get => newItemName; set => SetProperty(ref newItemName, value); }
        public ObservableCollection<Supplier> Suppliers { get; }

        private System.Collections.IEnumerable myItemsSource;

        public System.Collections.IEnumerable MyItemsSource { get => myItemsSource; set => SetProperty(ref myItemsSource, value); }
    }
}