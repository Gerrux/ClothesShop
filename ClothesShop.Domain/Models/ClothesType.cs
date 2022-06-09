using System.ComponentModel.DataAnnotations;

namespace ClothesShop.Domain.Models
{
    public class ClothesType
    {
        public Guid Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }
    }
}
