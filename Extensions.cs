
using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog{
    public static class Extensions{
        public static ItemDTO asDto(this Item item){
            return new ItemDTO{
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            Description = item.Description,
            CreatedAt = item.CreatedAt
            };
        }
    }
}