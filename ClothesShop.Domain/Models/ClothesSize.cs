using System.Text.Json.Serialization;

namespace ClothesShop.Domain.Models
{
    public enum Sizes
    {
        S = 44,
        M = 46,
        L = 48,
        XL = 50,
        XXL = 52
    }
    public class ClothesSize
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid ClothesId { get; set; }

        public Sizes Size { get; set; }

        public int Quantity { get; set; }
    }
}
