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
    internal class ClothesTypesPageViewModel : ObservableObject
    {
        public ClothesTypesPageViewModel(ObservableCollection<ClothesType> clothesTypes)
        => ClothesTypes = clothesTypes ?? throw new ArgumentNullException(nameof(clothesTypes));

        private static bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new DependencyObject());
        public ClothesTypesPageViewModel()
                : this(new ObservableCollection<ClothesType>())
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
            List<ClothesType> list;
            using (var connect = new DatabaseContext())
                list = connect.ClothesTypes.ToList();

            list.ForEach(clt => ClothesTypes.Add(clt));
            MyItemsSource = list;
        }

        private void SaveNewItem(object obj)
        {
            if (NewItemName == null)
                MessageBox.Show("Пустое название!");
            else
            {
                DatabaseContext _context = new();
                ClothesType clothesType = new() { Name = NewItemName };
                _context.ClothesTypes.Add(clothesType);
                _context.SaveChanges();
                MessageBox.Show($"Новый производитель: {NewItemName} успешно сохранён!");

                FillForm();
            }
        }

        private void UpdateItem(object parameter)
        {
            Guid idItem = (Guid)parameter;
            using DatabaseContext _context = new();
            ClothesType clothesType = _context.ClothesTypes.First(c => c.Id == idItem);
            UpdateClothesTypeModalWindow window = new(clothesType);
            window.ShowDialog();
            FillForm();
        }
        private void DeleteItem(object parameter)
        {
            Guid idItem = (Guid)parameter;
            using DatabaseContext _context = new();
            ClothesType clothesType = _context.ClothesTypes.First(c => c.Id == idItem);
            MessageBox.Show($"Удалён поставщик: {clothesType.Name}!");
            _context.ClothesTypes.Remove(clothesType);
            _context.SaveChanges();
            MyItemsSource = _context.ClothesTypes.ToList();
        }

        private string newItemName;
        public string NewItemName { get => newItemName; set => SetProperty(ref newItemName, value); }
        public ObservableCollection<ClothesType> ClothesTypes { get; }

        private System.Collections.IEnumerable myItemsSource;

        public System.Collections.IEnumerable MyItemsSource { get => myItemsSource; set => SetProperty(ref myItemsSource, value); }
    }
}
