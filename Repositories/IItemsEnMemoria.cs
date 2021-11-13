using System;
using System.Collections.Generic;
using Catalog.Entities;

namespace Catalog.Repositories{

public interface IItemsEnMemoria{
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
        void AddItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Guid id);
    }
}