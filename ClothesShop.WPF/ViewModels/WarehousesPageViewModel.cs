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
    internal class WarehousesPageViewModel : ObservableObject
    {
        public WarehousesPageViewModel(ObservableCollection<Warehouse> warehouses)
        => Warehouses = warehouses ?? throw new ArgumentNullException(nameof(warehouses));

        private static bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new DependencyObject());
        public WarehousesPageViewModel()
                : this(new ObservableCollection<Warehouse>())
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
            List<Warehouse> list;
            using (var connect = new DatabaseContext())
                list = connect.Warehouses.ToList();

            list.ForEach(clt => Warehouses.Add(clt));
            MyItemsSource = list;
        }
        private void SaveNewItem(object obj)
        {
            if (NewItemName == null)
                MessageBox.Show("Пустое название!");
            else
            {
                DatabaseContext _context = new();
                Warehouse warehouse = new() { Name = NewItemName };
                _context.Warehouses.Add(warehouse);
                _context.SaveChanges();
                MessageBox.Show($"Новый склад: {NewItemName} успешно сохранён!");

                FillForm();
            }
        }

        private void UpdateItem(object parameter)
        {
            Guid idItem = (Guid)parameter;
            using DatabaseContext _context = new();
            Warehouse warehouse = _context.Warehouses.First(c => c.Id == idItem);
            UpdateWarehouseModalWindow window = new(warehouse);
            window.ShowDialog();
            FillForm();
        }
        private void DeleteItem(object parameter)
        {
            Guid idItem = (Guid)parameter;
            using DatabaseContext _context = new();
            Warehouse warehouse = _context.Warehouses.First(c => c.Id == idItem);
            MessageBox.Show($"Удалён склад: {warehouse.Name}!");
            _context.Warehouses.Remove(warehouse);
            _context.SaveChanges();
            MyItemsSource = _context.Warehouses.ToList();
        }

        private string newItemName;
        public string NewItemName { get => newItemName; set => SetProperty(ref newItemName, value); }
        public ObservableCollection<Warehouse> Warehouses { get; }

        private System.Collections.IEnumerable myItemsSource;

        public System.Collections.IEnumerable MyItemsSource { get => myItemsSource; set => SetProperty(ref myItemsSource, value); }
    }
}
