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
    internal class UpdateClothesPageViewModel : ObservableObject
    {
        public ICommand AddNewPhotoCommand { get; set; }
        public ICommand DeletePhotoCommand { get; set; }
        public ICommand UpdateClothesCommand { get; set; }
        public void InitCommands(NavigationService mainFrame)
        {
            AddNewPhotoCommand = new DelegateCommand(c =>
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Image Files(*.PNG;*.BMP;*.JPG)|*.PNG;*.BMP;*.JPG|All files (*.*)|*.* ";
                if (dlg.ShowDialog() == true)
                {
                    string filepath = dlg.FileName;
                    ClothesImage image = new()
                    {
                        Clothes = Clothes,
                        ClothesId = ClothesId,
                        ImagePath = filepath
                    };
                    List<ClothesImage> list = new List<ClothesImage>();
                    if (MyImageItems is not null)
                    {
                        foreach (ClothesImage item in MyImageItems)
                        {
                            list.Add(item);
                        }
                    }
                    list.Add(image);
                    MyImageItems = list;
                }
            });
            DeletePhotoCommand = new DelegateCommand(DeletePhoto);
            UpdateClothesCommand = new DelegateCommand(c =>
            {
                UpdateClothes(c);
                mainFrame.Navigate(new ClothesPage());
            });
        }

        private void UpdateClothes(object parameter)
        {
            using DatabaseContext context = new();
            string descriptionClothes = ClothesDescription;
            if (descriptionClothes == "")
                descriptionClothes = "Нет описания";

            Clothes clothes = context.Clothes.FirstOrDefault(c => c.Id.Equals((Guid)parameter));
            clothes.Name = ClothesName;
            clothes.Description = ClothesDescription;
            clothes.ClothesTypeId = ((ClothesType)ClothesType).Id;
            clothes.Type = (ClothesType)ClothesType;
            clothes.Gender = (GenderNames)ClothesGender;
            clothes.Season = (Seasons)ClothesSeason;
            clothes.Cost = float.Parse(ClothesCost);
            clothes.WarehouseId = ((Warehouse)ClothesWarehouse).Id;
            clothes.Warehouse = (Warehouse)ClothesWarehouse;
            clothes.SupplierId = ((Supplier)ClothesSupplier).Id;
            clothes.Supplier = (Supplier)ClothesSupplier;
            clothes.ManufacturerId = ((Manufacturer)ClothesManufacturer).Id;
            clothes.Manufacturer = (Manufacturer)ClothesManufacturer;

            foreach (ClothesImage image in context.ClothesImages.Where(c => c.ClothesId.Equals(clothes.Id)).ToList())
            {
                context.ClothesImages.Remove(image);
            }

            clothes.Images = (List<ClothesImage>)(MyImageItems);
            context.Clothes.Update(clothes);

            ClothesSize sizeS = context.ClothesSizes.Where(c => c.ClothesId == clothes.Id)
                                                    .FirstOrDefault(c => c.Size.Equals(Sizes.S));
            sizeS.Quantity = int.Parse(CountSizeS);
            ClothesSize sizeM = context.ClothesSizes.Where(c => c.ClothesId == clothes.Id)
                                                    .FirstOrDefault(c => c.Size.Equals(Sizes.M));
            sizeM.Quantity = int.Parse(CountSizeM);
            ClothesSize sizeL = context.ClothesSizes.Where(c => c.ClothesId == clothes.Id)
                                                    .FirstOrDefault(c => c.Size.Equals(Sizes.L));
            sizeL.Quantity = int.Parse(CountSizeL);
            ClothesSize sizeXL = context.ClothesSizes.Where(c => c.ClothesId == clothes.Id)
                                                     .FirstOrDefault(c => c.Size.Equals(Sizes.XL));
            sizeXL.Quantity = int.Parse(CountSizeXL);
            ClothesSize sizeXXL = context.ClothesSizes.Where(c => c.ClothesId == clothes.Id)
                                                      .FirstOrDefault(c => c.Size.Equals(Sizes.XXL));
            sizeXXL.Quantity = int.Parse(CountSizeXXL);

            context.ClothesSizes.Update(sizeS);
            context.ClothesSizes.Update(sizeM);
            context.ClothesSizes.Update(sizeL);
            context.ClothesSizes.Update(sizeXL);
            context.ClothesSizes.Update(sizeXXL);
            context.SaveChanges();
            MessageBox.Show($"Экземпляр одежды: {clothes.Name} отредактирован");
        }
        private void DeletePhoto(object parameter)
        {
            List<ClothesImage> buferClothesImagesList = new();
            foreach (ClothesImage image in (List<ClothesImage>)(MyImageItems))
            {
                buferClothesImagesList.Add(image);
            }
            ClothesImage cltImg = buferClothesImagesList.Find(c => c.Id.Equals((Guid)parameter));
            buferClothesImagesList.Remove(cltImg);
            MyImageItems = buferClothesImagesList;
        }

        public UpdateClothesPageViewModel(ObservableCollection<ClothesImage> clothesImages)
        => ClothesImages = clothesImages ?? throw new ArgumentNullException(nameof(ClothesImage));
        public UpdateClothesPageViewModel(Guid clothesId, NavigationService mainFrame)
                : this(new ObservableCollection<ClothesImage>())
        {
            {
                InitCommands(mainFrame);
                using DatabaseContext context = new();
                ClothesManufacturers = context.Manufacturers.ToList();
                ClothesSuppliers = context.Suppliers.ToList();
                ClothesTypes = context.ClothesTypes.ToList();
                ClothesWarehouses = context.Warehouses.ToList();

                Clothes clothes = context.Clothes.FirstOrDefault(clt => clt.Id.Equals(clothesId));
                Clothes = clothes;
                ClothesId = clothesId;
                ClothesName = clothes.Name;
                ClothesDescription = clothes.Description;
                ClothesType = context.ClothesTypes.FirstOrDefault(clt => clt.Id.Equals(clothes.ClothesTypeId));
                ClothesGender = clothes.Gender;
                ClothesSeason = clothes.Season;
                ClothesCost = clothes.Cost.ToString();
                ClothesWarehouse = context.Warehouses.FirstOrDefault(clt => clt.Id.Equals(clothes.WarehouseId));
                ClothesSupplier = context.Suppliers.FirstOrDefault(clt => clt.Id.Equals(clothes.SupplierId));
                ClothesManufacturer = context.Manufacturers.FirstOrDefault(clt => clt.Id.Equals(clothes.ManufacturerId));

                var clothesSizes = context.ClothesSizes.Where(cs => cs.ClothesId == clothesId);
                CountSizeS = clothesSizes.First(clothesSize => clothesSize.Size.Equals(Sizes.S)).Quantity.ToString();
                CountSizeM = clothesSizes.First(clothesSize => clothesSize.Size.Equals(Sizes.M)).Quantity.ToString();
                CountSizeL = clothesSizes.First(clothesSize => clothesSize.Size.Equals(Sizes.L)).Quantity.ToString();
                CountSizeXL = clothesSizes.First(clothesSize => clothesSize.Size.Equals(Sizes.XL)).Quantity.ToString();
                CountSizeXXL = clothesSizes.First(clothesSize => clothesSize.Size.Equals(Sizes.XXL)).Quantity.ToString();

                MyImageItems = context.ClothesImages.Where(image => image.ClothesId.Equals(clothesId)).ToList();

            }
        }

        public ObservableCollection<ClothesImage> ClothesImages { get; }

        private System.Collections.IEnumerable myImageItems;

        public System.Collections.IEnumerable MyImageItems { get => myImageItems; set => SetProperty(ref myImageItems, value); }

        private Clothes clothes;
        public Clothes Clothes { get => clothes; set => SetProperty(ref clothes, value); }

        private Guid clothesId;

        public Guid ClothesId { get => clothesId; set => SetProperty(ref clothesId, value); }

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
