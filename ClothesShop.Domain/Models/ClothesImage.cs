using System.Text.Json.Serialization;

namespace ClothesShop.Domain.Models
{
    public class ClothesImage
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string ImagePath { get; set; }
        [JsonIgnore]
        public Guid ClothesId { get; set; }
        [JsonIgnore]

        public Clothes Clothes { get; set; }
    }
}
