namespace ClothesShop.Domain.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ClothesSizeId { get; set; }
        public ClothesSize Clothes { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
