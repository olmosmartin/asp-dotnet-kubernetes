using Catalog.Entities;
/*
using System.Collections.Generic;
using System;
*/

namespace Catalog.Repositories {
    public class ItemsEnMemoria {
        private readonly List<Item> items = new() {
            new Item {
                Id = Guid.NewGuid(),
                Name = "Item 1",
                Description = "Descripcion del item 1",
                Price = 10,
                CreatedAt = DateTimeOffset.UtcNow,
            },
            new Item {
                Id = Guid.NewGuid(),
                Name = "Item 2",
                Description = "Descripcion del item 2",
                Price = 15,
                CreatedAt = DateTimeOffset.UtcNow,
            },
            new Item {
                Id = Guid.NewGuid(),
                Name = "Item 2",
                Description = "Descripcion del item 2",
                Price = 20,
                CreatedAt = DateTimeOffset.UtcNow,
            },
        };

        public IEnumerable<Item> GetItems() => items;

        public Item GetItem(Guid id) => items.Find(x => x.Id == id);
    }
}