using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Product
{
    public class CreateOrUpdateProductDto
    {
        [Required]
        [MaxLength(255, ErrorMessage = "Maximum length for Product Name is 250 characters.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "Maximum length for Product Summary is 250 characters.")]
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
