namespace ClothesShop.Domain.Models
{
    public enum OrderStatus
    {
        NotCompleted,
        Completed,
        Cancelled
    }

    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public OrderStatus Status { get; set; }

        public float Price { get; set; }

        public DateTime OrderTime { get; set; }

        public DateTime? CompletedTime { get; set; }
    }
}
