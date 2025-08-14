using System.ComponentModel.DataAnnotations;

namespace ProductCaseArtius.Domain.Entities
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Category { get; set; } = string.Empty;
    }
}
