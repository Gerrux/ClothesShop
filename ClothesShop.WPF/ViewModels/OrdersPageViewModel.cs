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

namespace ClothesShop.WPF.ViewModels
{
    internal class OrdersPageViewModel : ObservableObject
    {
        public OrdersPageViewModel(ObservableCollection<Order> orders)
        => Orders = orders ?? throw new ArgumentNullException(nameof(orders));

        private static bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new DependencyObject());
        public OrdersPageViewModel()
                : this(new ObservableCollection<Order>())
        {
            //if (!IsInDesignMode)
            //{
            //    List<Order> list;
            //    using (var connect = new DatabaseContext())
            //        list = connect.Orders.Include(x => x.Size).ToList();
            //    foreach (Order order in list)
            //    {
            //        Orders.Add(order);
            //    }
            //    MyItemsSource = list;
            //}

        }

        public ObservableCollection<Order> Orders { get; }

        private System.Collections.IEnumerable myItemsSource;

        public System.Collections.IEnumerable MyItemsSource { get => myItemsSource; set => SetProperty(ref myItemsSource, value); }
    }
}
