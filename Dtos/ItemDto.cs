using System;

namespace Catalog.Dtos
{
    public record ItemDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}