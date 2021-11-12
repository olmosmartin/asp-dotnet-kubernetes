using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Entities;
using Catalog.Dtos;

namespace Catalog.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase{
        private readonly IItemsEnMemoria repository;

        public ItemsController(IItemsEnMemoria repository){
            this.repository = repository;
        }

        // GET api/items
        [HttpGet]
        public IEnumerable<ItemDTO> GetItems(){
            var items = repository.GetItems().Select (item => item.asDto());
            return items;
        }

        // GET api/items/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemDTO> GetItem(Guid id){
            var item = repository.GetItem(id);
            if(item is null){
                return NotFound();
            } else {
                return item.asDto();
            }
        }

    }
        
}
