using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using ClothesShop.WPF.Views.Pages;
using GRRX.ComponentModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ClothesShop.WPF.ViewModels
{
    internal class CreateClothesPageViewModel : ObservableObject
    {
        public ICommand AddNewPhotoCommand { get; set; }
        public ICommand DeletePhotoCommand { get; set; }
        public ICommand SaveNewClothesCommand { get; set; }
        public void InitCommands(NavigationService mainFrame)
        {
            AddNewPhotoCommand = new DelegateCommand(c =>
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Image Files(*.PNG;*.BMP;*.JPG)|*.PNG;*.BMP;*.JPG|All files (*.*)|*.* ";
                if (dlg.ShowDialog() == true)
                {
                    string filepath = dlg.FileName;
                    ImagePath image = new()
                    {
                        Id = Guid.NewGuid(),
                        PhotoPath = filepath
                    };
                    List<ImagePath> list = new List<ImagePath>();
                    if (MyImageItems is not null)
                    {
                        foreach (ImagePath item in MyImageItems)
                        {
                            list.Add(item);
                        }
                    }
                    list.Add(image);
                    MyImageItems = list;
                }
            });

            DeletePhotoCommand = new DelegateCommand(DeletePhoto);

            SaveNewClothesCommand = new DelegateCommand(c =>
            {
                using DatabaseContext context = new();
                string descriptionClothes = ClothesDescription;
                if (descriptionClothes == "")
                    descriptionClothes = "Нет описания";
                Clothes clothes = new()
                {
                    Name = ClothesName,
                    Description = descriptionClothes,
                    ClothesTypeId = (Guid)ClothesType,
                    Type = context.ClothesTypes.First(item => item.Id.Equals((Guid)ClothesType)),
                    Gender = (GenderNames)ClothesGender,
                    Season = (Seasons)ClothesSeason,
                    Cost = float.Parse(ClothesCost),
                    WarehouseId = (Guid)ClothesWarehouse,
                    Warehouse = context.Warehouses.First(item => item.Id.Equals((Guid)ClothesWarehouse)),
                    SupplierId = (Guid)ClothesSupplier,
                    Supplier = context.Suppliers.First(item => item.Id.Equals((Guid)ClothesSupplier)),
                    ManufacturerId = (Guid)ClothesManufacturer,
                    Manufacturer = context.Manufacturers.First(item => item.Id.Equals((Guid)ClothesManufacturer)),
                };
                context.Clothes.Add(clothes);
                if (MyImageItems != null)
                {
                    foreach (ImagePath item in MyImageItems)
                    {
                        ClothesImage image = new()
                        {
                            ImagePath = item.PhotoPath,
                            ClothesId = clothes.Id,
                            Clothes = clothes
                        };
                        context.ClothesImages.Add(image);
                    }
                }
                else
                {
                    ClothesImage image = new()
                    {
                        ImagePath = @"C:\Users\kalin\Source\Repos\ClothesShop\ClothesShop.WPF\Source\Images\placeholder.png",
                        ClothesId = clothes.Id,
                        Clothes = clothes
                    };
                    context.ClothesImages.Add(image);
                }
                ClothesSize sizeS = new()
                {
                    ClothesId = clothes.Id,
                    Size = Sizes.S,
                    Quantity = int.Parse(CountSizeS)
                };
                ClothesSize sizeM = new()
                {
                    ClothesId = clothes.Id,
                    Size = Sizes.M,
                    Quantity = int.Parse(CountSizeM)
                };
                ClothesSize sizeL = new()
                {
                    ClothesId = clothes.Id,
                    Size = Sizes.L,
                    Quantity = int.Parse(CountSizeL)
                };
                ClothesSize sizeXL = new()
                {
                    ClothesId = clothes.Id,
                    Size = Sizes.XL,
                    Quantity = int.Parse(CountSizeXL)
                };
                ClothesSize sizeXXL = new()
                {
                    ClothesId = clothes.Id,
                    Size = Sizes.XXL,
                    Quantity = int.Parse(CountSizeXXL)
                };
                context.ClothesSizes.Add(sizeS);
                context.ClothesSizes.Add(sizeM);
                context.ClothesSizes.Add(sizeL);
                context.ClothesSizes.Add(sizeXL);
                context.ClothesSizes.Add(sizeXXL);
                context.SaveChanges();
                MessageBox.Show($"Новый экземпляр одежды: {clothes.Name} создан");
                mainFrame.Navigate(new ClothesPage());
            });
        }

        private void DeletePhoto(object parameter)
        {
            List<ImagePath> buferImagePathsList = (List<ImagePath>)(MyImageItems);
            ImagePath imgPath = buferImagePathsList.Find(c => c.Id.Equals((Guid)parameter));
            buferImagePathsList.Remove(imgPath);

            MyImageItems = buferImagePathsList;
        }

        public class ImagePath
        {
            public Guid Id { get; set; }
            public string PhotoPath { get; set; }

        }
        public CreateClothesPageViewModel(ObservableCollection<ImagePath> imagePaths)
        => ImagePaths = imagePaths ?? throw new ArgumentNullException(nameof(imagePaths));
        public CreateClothesPageViewModel(NavigationService mainFrame)
        {
            InitCommands(mainFrame);
            using DatabaseContext context = new();
            ClothesManufacturers = context.Manufacturers.ToList();
            ClothesSuppliers = context.Suppliers.ToList();
            ClothesTypes = context.ClothesTypes.ToList();
            ClothesWarehouses = context.Warehouses.ToList();
        }
        public ObservableCollection<ImagePath> ImagePaths { get; }

        private System.Collections.IEnumerable myImageItems;

        public System.Collections.IEnumerable MyImageItems { get => myImageItems; set => SetProperty(ref myImageItems, value); }

        private string name;

        public string ClothesName { get => name; set => SetProperty(ref name, value); }

        private string description;

        public string ClothesDescription { get => description; set => SetProperty(ref description, value); }

        private object gender;

        public object ClothesGender { get => gender; set => SetProperty(ref gender, value); }

        private object clothesType;

        public object ClothesType { get => clothesType; set => SetProperty(ref clothesType, value); }

        private object manufacturer;

        public object ClothesManufacturer { get => manufacturer; set => SetProperty(ref manufacturer, value); }

        private object season;

        public object ClothesSeason { get => season; set => SetProperty(ref season, value); }

        private object warehouse;

        public object ClothesWarehouse { get => warehouse; set => SetProperty(ref warehouse, value); }

        private object supplier;

        public object ClothesSupplier { get => supplier; set => SetProperty(ref supplier, value); }

        private string cost;

        public string ClothesCost { get => cost; set => SetProperty(ref cost, value); }

        private string countSizeS = "0";

        public string CountSizeS { get => countSizeS; set => SetProperty(ref countSizeS, value); }

        private string countSizeM = "0";

        public string CountSizeM { get => countSizeM; set => SetProperty(ref countSizeM, value); }

        private string countSizeL = "0";

        public string CountSizeL { get => countSizeL; set => SetProperty(ref countSizeL, value); }

        private string countSizeXL = "0";

        public string CountSizeXL { get => countSizeXL; set => SetProperty(ref countSizeXL, value); }

        private string countSizeXXL = "0";

        public string CountSizeXXL { get => countSizeXXL; set => SetProperty(ref countSizeXXL, value); }

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