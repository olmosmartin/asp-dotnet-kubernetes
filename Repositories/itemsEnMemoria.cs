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

        public async Task<IEnumerable<Item>> GetItemsAsync() => await Task.FromResult(items);

        public async Task<Item> GetItemAsync(Guid id) => await Task.FromResult(items.SingleOrDefault(x => x.Id == id));
        public async Task AddItemAsync(Item item) {
            items.Add(item);
            await Task.CompletedTask;
        } 
        public async Task UpdateItemAsync(Item item)
        {
            var itemIndex = items.FindIndex(itm => itm.Id == item.Id);
            items[itemIndex] = item;
            await Task.CompletedTask;
        }
        public async Task DeleteItemAsync(Guid id){
            items.RemoveAll(x => x.Id == id);
            await Task.CompletedTask;
        }
    }
}