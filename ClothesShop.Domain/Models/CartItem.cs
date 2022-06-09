namespace ClothesShop.Domain.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        public Guid ClothesSizeId { get; set; }
        public ClothesSize Clothes { get; set; }
        public int Quantity { get; set; }
    }
}
