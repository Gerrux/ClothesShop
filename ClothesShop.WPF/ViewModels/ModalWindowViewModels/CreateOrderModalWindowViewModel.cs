using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using GRRX.ComponentModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ClothesShop.WPF.ViewModels.ModalWindowViewModels
{
    internal class CreateOrderModalWindowViewModel : ObservableObject
    {
        public CreateOrderModalWindowViewModel(Guid clothesId)
        {
            using DatabaseContext context = new();
            OrderedClothes = context.Clothes.Include(x => x.Sizes).FirstOrDefault(c => c.Id.Equals(clothesId));
            ClothesName = OrderedClothes.Name;
            List<Sizes> listSizes = new();
            foreach (ClothesSize size in OrderedClothes.Sizes)
            {
                if (size.Quantity > 0)
                    listSizes.Add(size.Size);
            }
            SourceSizes = listSizes;

            OrderCommand = new DelegateCommand(c =>
            {
                //using DatabaseContext context = new();
                //ClothesSize size = context.ClothesSizes.Where(c => c.ClothesId.Equals(OrderedClothes.Id))
                //                               .FirstOrDefault(c => c.Size.Equals(Size));
                //if (size.Quantity >= Quantity)
                //{
                //    Order order = new()
                //    {
                //        Status = OrderStatus.NotCompleted,
                //        Price = OrderedClothes.Cost * Quantity,
                //        Size = size,
                //        Quantity = Quantity,
                //        ClothesId = OrderedClothes.Id,
                //        OrderTime = DateTime.Now,
                //        CompletedTime = null
                //    };
                //    size.Quantity -= Quantity;
                //    context.ClothesSizes.Update(size);
                //    context.Orders.Add(order);
                //    context.SaveChanges();
                //    CloseAction();
                //}
                //else
                //    MessageBox.Show($"На складе всего: {size.Quantity} ед.");
            });
        }

        public List<Sizes> SourceSizes { get; set; }

        public Action CloseAction { get; set; }
        public ICommand OrderCommand { get; set; }
        public string ClothesName { get; set; }
        public int Quantity { get; set; }
        public Sizes Size { get; set; }
        public Clothes OrderedClothes { get; set; }
    }
}
