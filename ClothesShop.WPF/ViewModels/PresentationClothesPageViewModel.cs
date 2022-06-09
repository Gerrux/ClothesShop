using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using ClothesShop.WPF.Views.ModalWindows;
using ClothesShop.WPF.Views.Pages;
using GRRX.ComponentModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ClothesShop.WPF.ViewModels
{
    internal class PresentationClothesPageViewModel : ObservableObject
    {
        public PresentationClothesPageViewModel(ObservableCollection<PresentClothes> presentClothes)
        => PresentsClothes = presentClothes ?? throw new ArgumentNullException(nameof(presentClothes));

        private static bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new DependencyObject());

        public PresentationClothesPageViewModel(Guid presentClothesId, NavigationService mainFrame) : this(new ObservableCollection<PresentClothes>())
        {
            InitCommands(presentClothesId, mainFrame);
            using DatabaseContext context = new();
            Clothes clothes = context.Clothes.FirstOrDefault(c => c.Id.Equals(presentClothesId));
            clothesId = presentClothesId.ToString();
            clothesName = clothes.Name;
            clothesCost = clothes.Cost.ToString();
            clothesDescription = clothes.Description;
            clothesType = context.ClothesTypes.FirstOrDefault(c => c.Id.Equals(clothes.ClothesTypeId)).Name;
            clothesGender = clothes.Gender.ToString();
            clothesSeason = clothes.Season.ToString();
            clothesManufacturer = context.Manufacturers.FirstOrDefault(c => c.Id.Equals(clothes.ManufacturerId)).Name;
            clothesWarehouse = context.Warehouses.FirstOrDefault(c => c.Id.Equals(clothes.WarehouseId)).Name;
            clothesSupplier = context.Suppliers.FirstOrDefault(c => c.Id.Equals(clothes.SupplierId)).Name;
            clothesImages = context.ClothesImages.Where(c => c.ClothesId.Equals(presentClothesId)).ToList();
            if (!clothesImages.Any())
                clothesImages.Add(new ClothesImage { Clothes = clothes, ClothesId = clothes.Id, ImagePath = @"C:\Users\kalin\source\repos\ClothesShop.WPF\ClothesShop.WPF\Source\Images\placeholder.png" });
            photoPath = clothesImages[0].ImagePath;


            InitRecomCommands();
            if (!IsInDesignMode)
            {
                List<Clothes> list = context.Clothes.ToList();
                List<PresentClothes> presentList = new();
                foreach (Clothes recclothes in list)
                {
                    if (!recclothes.Id.Equals(presentClothesId))
                    {
                        List<ClothesImage> clothesImages = context.ClothesImages.Where(c => c.ClothesId.Equals(recclothes.Id)).ToList();
                        if (!clothesImages.Any())
                            clothesImages.Add(new ClothesImage { Clothes = recclothes, ClothesId = recclothes.Id, ImagePath = @"C:\Users\kalin\source\repos\ClothesShop.WPF\ClothesShop.WPF\Source\Images\placeholder.png" });
                        PresentClothes presentClothes = new PresentClothes
                        {
                            Id = presentList.Count,
                            Name = recclothes.Name,
                            Cost = recclothes.Cost.ToString(),
                            Clothes = recclothes,
                            ClothesImages = clothesImages,
                            CurrentImagePath = clothesImages[0].ImagePath,
                            CurrentImageIndex = 0,
                            ClothesId = recclothes.Id
                        };
                        presentList.Add(presentClothes);
                        PresentsClothes.Add(presentClothes);
                    }
                    if (presentList.Count == 5)
                        break;

                };
                MyItemsSource = presentList;
            }
        }
        public List<ClothesImage> clothesImages { get; set; }
        public ICommand CreateOrderCommand { get; set; }
        public ICommand UpdateClothesCommand { get; set; }
        public ICommand DeleteClothesCommand { get; set; }
        public ICommand PrevImageCommand { get; set; }
        public ICommand FollowImageCommand { get; set; }
        public void InitCommands(Guid presentClothesId, NavigationService mainFrame)
        {
            int indexImage = 0;

            CreateOrderCommand = new DelegateCommand(c =>
            {
                using DatabaseContext context = new();
                List<ClothesSize> clothesSizes = context.ClothesSizes.Where(c => c.ClothesId.Equals(presentClothesId)).ToList();
                int quantity = 0;
                foreach (ClothesSize clothesSize in clothesSizes)
                    quantity += clothesSize.Quantity;
                if (quantity > 0)
                {
                    CreateOrderModalWindow window = new(presentClothesId);
                    window.ShowDialog();
                }
                else
                    MessageBox.Show("Товара нет в наличии!");
            });

            PrevImageCommand = new DelegateCommand(c =>
            {
                indexImage--;
                if (indexImage < 0)
                    indexImage = clothesImages.Count - 1;
                PhotoPath = clothesImages[indexImage].ImagePath;
            });

            FollowImageCommand = new DelegateCommand(c =>
            {
                indexImage++;
                if (indexImage == clothesImages.Count)
                    indexImage = 0;
                PhotoPath = clothesImages[indexImage].ImagePath;
            });

            UpdateClothesCommand = new DelegateCommand(c => mainFrame.Navigate(new UpdateClothesPage(presentClothesId, mainFrame)));

            DeleteClothesCommand = new DelegateCommand(c =>
            {
                using DatabaseContext context = new();
                Clothes clothes = context.Clothes.First(c => c.Id.Equals(presentClothesId));
                context.Clothes.Remove(clothes);
                context.SaveChanges();
                MessageBox.Show($"Одежда: {ClothesName} удалена");
                mainFrame.Navigate(new ClothesPage());
            });
        }


        private string clothesId;

        public string ClothesId { get => clothesId; set => SetProperty(ref clothesId, value); }

        private string clothesName;

        public string ClothesName { get => clothesName; set => SetProperty(ref clothesName, value); }

        private string clothesCost;

        public string ClothesCost { get => clothesCost; set => SetProperty(ref clothesCost, value); }

        private string clothesDescription;

        public string ClothesDescription { get => clothesDescription; set => SetProperty(ref clothesDescription, value); }

        private string clothesType;

        public string ClothesType { get => clothesType; set => SetProperty(ref clothesType, value); }

        private string clothesGender;

        public string ClothesGender { get => clothesGender; set => SetProperty(ref clothesGender, value); }

        private string clothesSeason;

        public string ClothesSeason { get => clothesSeason; set => SetProperty(ref clothesSeason, value); }

        private string clothesManufacturer;

        public string ClothesManufacturer { get => clothesManufacturer; set => SetProperty(ref clothesManufacturer, value); }

        private string clothesWarehouse;

        public string ClothesWarehouse { get => clothesWarehouse; set => SetProperty(ref clothesWarehouse, value); }

        private string clothesSupplier;

        public string ClothesSupplier { get => clothesSupplier; set => SetProperty(ref clothesSupplier, value); }

        private string photoPath;

        public string PhotoPath { get => photoPath; set => SetProperty(ref photoPath, value); }



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
        public void InitRecomCommands()
        {
            RecomPrevImageCommand = new DelegateCommand(PrevImage);
            RecomFollowImageCommand = new DelegateCommand(FollowImage);
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
        public ICommand RecomPrevImageCommand { get; set; }
        public ICommand RecomFollowImageCommand { get; set; }
        public ObservableCollection<PresentClothes> PresentsClothes { get; }

        private System.Collections.IEnumerable myItemsSource;

        public System.Collections.IEnumerable MyItemsSource { get => myItemsSource; set => SetProperty(ref myItemsSource, value); }
    }
}
