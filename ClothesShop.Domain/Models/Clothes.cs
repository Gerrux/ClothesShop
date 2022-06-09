using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClothesShop.Domain.Models
{
    public enum GenderNames
    {
        Unisex,
        Man,
        Woman,
        Other
    }
    public enum Seasons
    {
        Allseasons,
        Summer,
        Demi,
        Winter
    }
    public class Clothes
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public ICollection<ClothesImage> Images { get; set; }
        public string Description { get; set; }
        [Required]
        [JsonIgnore]
        public Guid ClothesTypeId { get; set; }
        public ClothesType Type { get; set; }
        [Required]
        public GenderNames Gender { get; set; }
        [Required]
        public Seasons Season { get; set; }
        public ICollection<ClothesSize> Sizes { get; set; }
        [Required]
        public float Cost { get; set; }
        [Required]
        [JsonIgnore]
        public Guid WarehouseId { get; set; }
        [JsonIgnore]
        public Warehouse Warehouse { get; set; }
        [Required]
        [JsonIgnore]
        public Guid SupplierId { get; set; }
        [JsonIgnore]
        public Supplier Supplier { get; set; }
        [Required]
        [JsonIgnore]
        public Guid ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
