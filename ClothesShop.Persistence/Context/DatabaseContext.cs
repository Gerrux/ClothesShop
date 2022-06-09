
using ClothesShop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.Persistence.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Clothes> Clothes { get; set; }

        public DbSet<ClothesType> ClothesTypes { get; set; }

        public DbSet<ClothesImage> ClothesImages { get; set; }

        public DbSet<ClothesSize> ClothesSizes { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Cart> Carts { get; set; }


        public DatabaseContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=root;database=Clothesshop;",
                new MySqlServerVersion(new Version(8, 0, 29))
            );
        }
    }
}
