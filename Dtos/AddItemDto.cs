using System.ComponentModel.DataAnnotations;

namespace Catalog.Dtos
{
    public record AddItemDto
    {
        [Required]
        public string Name { get; init; }
        public string Description { get; init; }
        [Required]
        [Range(1, 10000)]
        public decimal Price { get; init; }
    }
}