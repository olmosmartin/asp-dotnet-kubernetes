using Catalog.Entities;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Catalog.Repositories {

    public class ItemsEnMemoria : IItemsEnMemoria
    {
        private readonly List<Item> items = new()
        {
            new Item
            {
                Id = Guid.NewGuid(),
                Name = "Item 1",
                Description = "Descripcion del item 1",
                Price = 10,
                CreatedAt = DateTimeOffset.UtcNow,
            },
            new Item
            {
                Id = Guid.NewGuid(),
                Name = "Item 2",
                Description = "Descripcion del item 2",
                Price = 15,
                CreatedAt = DateTimeOffset.UtcNow,
            },
            new Item
            {
                Id = Guid.NewGuid(),
                Name = "Item 2",
                Description = "Descripcion del item 2",
                Price = 20,
                CreatedAt = DateTimeOffset.UtcNow,
            },
        };

        public IEnumerable<Item> GetItems() => items;

        public Item GetItem(Guid id) => items.Find(x => x.Id == id);
        public void AddItem(Item item) => items.Add(item);
        public void UpdateItem(Item item)
        {
            var itemIndex = items.FindIndex(itm => itm.Id == item.Id);
            items[itemIndex] = item;
        }
        public void DeleteItem(Guid id) => items.RemoveAll(x => x.Id == id);
    }
}