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

        // POST api/items
        [HttpPost]
        public ActionResult<ItemDTO> PostItem(AddItemDto itemDTO){
            Item item = new(){
                Id = Guid.NewGuid(),
                Name = itemDTO.Name,
                Description = itemDTO.Description,
                Price = itemDTO.Price,
            };
            repository.AddItem(item);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.asDto());
        }

        // PUT api/items/{id}
        [HttpPut("{id}")]
        public ActionResult PutItem(Guid id, UpdateItemDto itemDTO)
        {
            var existingItem = repository.GetItem(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            /*  otra forma usando with que toma el que existe y le cambia los valores
                Item udatedItem = existingItem.with{
                    Name = itemDTO.Name,
                    Description = itemDTO.Description,
                    Price = itemDTO.Price,
                };
            */
            Item udatedItem = new(){
                Id = id,
                Name = itemDTO.Name,
                Description = itemDTO.Description,
                Price = itemDTO.Price,
            };
            repository.UpdateItem(udatedItem);
            return NoContent();
        }

        // DELETE api/items/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id){
            var existingItem = repository.GetItem(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            repository.DeleteItem(id);
            return NoContent();
        }
    }
        
}
