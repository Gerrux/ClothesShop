using System.ComponentModel.DataAnnotations;

namespace ClothesShop.Domain.Models
{
    public class Warehouse
    {
        public Guid Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }
    }
}
