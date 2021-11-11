using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Entities;

namespace Catalog.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase{
        private readonly ItemsEnMemoria repository;

        public ItemsController(){
            repository = new ItemsEnMemoria();
        }

        // GET api/items
        [HttpGet]
        public IEnumerable<Item> GetItems(){
            var items = repository.GetItems();
            return items;
        }

        // GET api/items/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id){
            var item = repository.GetItem(id);
            if(item is null){
                return NotFound();
            } else {
                return item;
            }
        }

    }
        
}
