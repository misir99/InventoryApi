using System.ComponentModel.DataAnnotations;

namespace InventoryApi.Dtos
{
    public class ProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, 1000000)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Range(0, 1000000)]
        public int Quantity { get; set; }
    }
}
