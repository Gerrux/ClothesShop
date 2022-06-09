using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClothesShop.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Surname { get; set; }
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
        [Required]
        [StringLength(200)]
        [JsonIgnore]
        public string Password { get; set; }

        public string Status { get; set; }
    }

    public enum StatusAccount
    {
        Active,
        Deleted
    }
}
