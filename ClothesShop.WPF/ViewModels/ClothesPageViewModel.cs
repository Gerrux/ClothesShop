using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using GRRX.ComponentModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ClothesShop.WPF.ViewModels
{
    internal class ClothesPageViewModel : ObservableObject
    {
        public ClothesPageViewModel(ObservableCollection<PresentClothes> presentClothes)
        => PresentsClothes = presentClothes ?? throw new ArgumentNullException(nameof(presentClothes));

        private static bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new DependencyObject());
        public ClothesPageViewModel()
                : this(new ObservableCollection<PresentClothes>())
        {
            InitCommands();
            if (!IsInDesignMode)
            {
                DatabaseContext _context = new();
                ClothesManufacturers = _context.Manufacturers.ToList();
                ClothesSuppliers = _context.Suppliers.ToList();
                ClothesTypes = _context.ClothesTypes.ToList();
                ClothesWarehouses = _context.Warehouses.ToList();
                List<Clothes> list = _context.Clothes.Include(clt => clt.Supplier).ToList();
                List<PresentClothes> presentList = new();
                foreach (Clothes clothes in list)
                {
                    List<ClothesImage> clothesImages = _context.ClothesImages.Where(c => c.ClothesId.Equals(clothes.Id)).ToList();
                    if (!clothesImages.Any())
                        clothesImages.Add(new ClothesImage { Clothes = clothes, ClothesId = clothes.Id, ImagePath = @"C:\Users\kalin\source\repos\ClothesShop\ClothesShop.WPF\Source\Images\placeholder.png" });
                    PresentClothes presentClothes = new PresentClothes
                    {
                        Id = presentList.Count,
                        Name = clothes.Name,
                        Cost = clothes.Cost.ToString(),
                        Clothes = clothes,
                        ClothesImages = clothesImages,
                        CurrentImagePath = clothesImages[0].ImagePath,
                        CurrentImageIndex = 0,
                        ClothesId = clothes.Id
                    };
                    presentList.Add(presentClothes);
                    PresentsClothes.Add(presentClothes);
                };
                MyItemsSource = presentList;
            }
        }

        public void SearchItems(string search)
        {
            DatabaseContext _context = new();
            List<Clothes> list;
            list = _context.Clothes.Where(c => c.Name.Contains(search)).ToList();
            List<PresentClothes> presentList = new();
            foreach (Clothes clothes in list)
            {
                List<ClothesImage> clothesImages = _context.ClothesImages.Where(c => c.ClothesId.Equals(clothes.Id)).ToList();
                PresentClothes presentClothesItem = new PresentClothes
                {
                    Id = presentList.Count,
                    Name = clothes.Name,
                    Cost = clothes.Cost.ToString(),
                    Clothes = clothes,
                    ClothesImages = clothesImages,
                    CurrentImagePath = clothesImages[0].ImagePath,
                    CurrentImageIndex = 0,
                    ClothesId = clothes.Id
                };
                presentList.Add(presentClothesItem);
                PresentsClothes.Add(presentClothesItem);
            };
            MyItemsSource = presentList;
        }

        public class PresentClothes
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Cost { get; set; }
            public Clothes Clothes { get; set; }
            public List<ClothesImage> ClothesImages { get; set; }
            public string CurrentImagePath { get; set; }
            public int CurrentImageIndex { get; set; }
            public Guid ClothesId { get; set; }
        }
        public void InitCommands()
        {
            bool isSortedDown = false;
            SortItemsCommand = new DelegateCommand(c =>
            {
                if (!isSortedDown)
                {
                    isSortedDown = true;
                    MyItemsSource = PresentsClothes.ToList().OrderBy(item => item.Cost);
                }
                else
                {
                    isSortedDown = false;
                    MyItemsSource = PresentsClothes.ToList().OrderByDescending(item => item.Cost);
                }
            });
            PrevImageCommand = new DelegateCommand(PrevImage);
            FollowImageCommand = new DelegateCommand(FollowImage);
        }
        private void PrevImage(object parameter)
        {
            List<PresentClothes> buferList = PresentsClothes.ToList();
            PresentClothes bufClothes = buferList.Find(c => c.Id == (int)parameter);
            bufClothes.CurrentImageIndex -= 1;
            if (bufClothes.CurrentImageIndex < 0)
                bufClothes.CurrentImageIndex = bufClothes.ClothesImages.Count() - 1;
            bufClothes.CurrentImagePath = bufClothes.ClothesImages[bufClothes.CurrentImageIndex].ImagePath;
            MyItemsSource = buferList;
        }
        private void FollowImage(object parameter)
        {
            List<PresentClothes> buferList = PresentsClothes.ToList();
            PresentClothes bufClothes = buferList.Find(c => c.Id == (int)parameter);
            bufClothes.CurrentImageIndex += 1;
            if (bufClothes.CurrentImageIndex >= bufClothes.ClothesImages.Count())
                bufClothes.CurrentImageIndex = 0;
            bufClothes.CurrentImagePath = bufClothes.ClothesImages[bufClothes.CurrentImageIndex].ImagePath;
            MyItemsSource = buferList;
        }
        public ICommand PrevImageCommand { get; set; }
        public ICommand FollowImageCommand { get; set; }
        public ICommand SortItemsCommand { get; set; }
        public ObservableCollection<PresentClothes> PresentsClothes { get; }

        private System.Collections.IEnumerable myItemsSource;

        public System.Collections.IEnumerable MyItemsSource { get => myItemsSource; set => SetProperty(ref myItemsSource, value); }

        private string searchString;
        public string SearchString
        {
            get => searchString;
            set
            {
                SetProperty(ref searchString, value);
                SearchItems(value);
            }
        }


        private object gender;
        public object ClothesGender
        {
            get => gender;
            set
            {
                if (!value.Equals(GenderNames.Unisex))
                {
                    MyItemsSource = PresentsClothes.ToList()
                        .Where(clt => clt.Clothes.Gender.Equals(value) || clt.Clothes.Gender.Equals(GenderNames.Unisex));
                }
                else
                {
                    MyItemsSource = PresentsClothes.ToList()
                            .Where(clt => clt.Clothes.Gender.Equals(value));
                }


                SetProperty(ref gender, value);
            }
        }

        private object clothesType;

        public object ClothesType
        {
            get => clothesType;
            set
            {
                MyItemsSource = PresentsClothes.ToList()
                    .Where(clt => clt.Clothes.ClothesTypeId.Equals(value));
                SetProperty(ref clothesType, value);
            }
        }

        private object manufacturer;

        public object ClothesManufacturer
        {
            get => manufacturer;
            set
            {
                MyItemsSource = PresentsClothes.ToList()
                    .Where(clt => clt.Clothes.ManufacturerId.Equals(value));
                SetProperty(ref manufacturer, value);
            }
        }

        private object season;

        public object ClothesSeason
        {
            get => season;
            set
            {
                MyItemsSource = PresentsClothes.ToList()
                            .Where(clt => clt.Clothes.Season.Equals(value));
                SetProperty(ref season, value);
            }
        }

        private object warehouse;

        public object ClothesWarehouse
        {
            get => warehouse;
            set
            {
                MyItemsSource = PresentsClothes.ToList()
                    .Where(clt => clt.Clothes.WarehouseId.Equals(value));
                SetProperty(ref warehouse, value);
            }
        }

        private object supplier;

        public object ClothesSupplier
        {
            get => supplier;
            set
            {
                MyItemsSource = PresentsClothes.ToList()
                    .Where(clt => clt.Clothes.SupplierId.Equals(value));
                SetProperty(ref supplier, value);
            }
        }




        private System.Collections.IEnumerable clothesTypes;

        public System.Collections.IEnumerable ClothesTypes { get => clothesTypes; set => SetProperty(ref clothesTypes, value); }

        private System.Collections.IEnumerable clothesManufacturers;

        public System.Collections.IEnumerable ClothesManufacturers { get => clothesManufacturers; set => SetProperty(ref clothesManufacturers, value); }

        private System.Collections.IEnumerable clothesWarehouses;

        public System.Collections.IEnumerable ClothesWarehouses { get => clothesWarehouses; set => SetProperty(ref clothesWarehouses, value); }

        private System.Collections.IEnumerable clothesSuppliers;

        public System.Collections.IEnumerable ClothesSuppliers { get => clothesSuppliers; set => SetProperty(ref clothesSuppliers, value); }
    }
}
