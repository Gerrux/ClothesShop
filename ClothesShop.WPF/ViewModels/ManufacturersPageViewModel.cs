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
    internal class ManufacturersPageViewModel : ObservableObject
    {
        public ManufacturersPageViewModel(ObservableCollection<Manufacturer> manufacturers)
        => Manufacturers = manufacturers ?? throw new ArgumentNullException(nameof(manufacturers));

        private static bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new DependencyObject());
        public ManufacturersPageViewModel()
                : this(new ObservableCollection<Manufacturer>())
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
            List<Manufacturer> list;
            using (var connect = new DatabaseContext())
                list = connect.Manufacturers.ToList();

            list.ForEach(clt => Manufacturers.Add(clt));
            MyItemsSource = list;
        }
        private void SaveNewItem(object obj)
        {
            if (NewItemName == null)
                MessageBox.Show("Пустое название!");
            else
            {
                DatabaseContext _context = new();
                Manufacturer manufacturer = new() { Name = NewItemName };
                _context.Manufacturers.Add(manufacturer);
                _context.SaveChanges();
                MessageBox.Show($"Новый производитель: {NewItemName} успешно сохранён!");

                FillForm();
            }
        }


        private void UpdateItem(object parameter)
        {
            Guid idItem = (Guid)parameter;
            using DatabaseContext _context = new();
            Manufacturer manufacturer = _context.Manufacturers.First(c => c.Id == idItem);
            UpdateManufacturerModalWindow window = new(manufacturer);
            window.ShowDialog();
            FillForm();
        }
        private void DeleteItem(object parameter)
        {
            Guid idItem = (Guid)parameter;
            using DatabaseContext _context = new();
            Manufacturer manufacturer = _context.Manufacturers.First(c => c.Id == idItem);
            MessageBox.Show($"Удалён производитель: {manufacturer.Name}!");
            _context.Manufacturers.Remove(manufacturer);
            _context.SaveChanges();
            MyItemsSource = _context.Manufacturers.ToList();
        }

        private string newItemName;
        public string NewItemName { get => newItemName; set => SetProperty(ref newItemName, value); }

        public ObservableCollection<Manufacturer> Manufacturers { get; }

        private System.Collections.IEnumerable myItemsSource;

        public System.Collections.IEnumerable MyItemsSource { get => myItemsSource; set => SetProperty(ref myItemsSource, value); }
    }
}
