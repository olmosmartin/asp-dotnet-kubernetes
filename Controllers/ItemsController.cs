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
        public async Task<IEnumerable<ItemDTO>> GetItemsAsync(){
            var items = (await repository.GetItemsAsync())
                        .Select (item => item.asDto());
            return items;
        }

        // GET api/items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItemAsync(Guid id){
            var item = await repository.GetItemAsync(id);
            if(item is null){
                return NotFound();
            } else {
                return item.asDto();
            }
        }

        // POST api/items
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> PostItemAsync(AddItemDto itemDTO){
            Item item = new(){
                Id = Guid.NewGuid(),
                Name = itemDTO.Name,
                Description = itemDTO.Description,
                Price = itemDTO.Price,
            };
            await repository.AddItemAsync(item);
            //en program se agrego option.SuppressAsyncSuffixInActionNames porque no se puede usar async en el nombre de la accion y despues llamarla
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.asDto());
        }

        // PUT api/items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutItemAsync(Guid id, UpdateItemDto itemDTO)
        {
            var existingItem = await repository.GetItemAsync(id);
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
            await repository.UpdateItemAsync(udatedItem);
            return NoContent();
        }

        // DELETE api/items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id){
            var existingItem = await repository.GetItemAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            await repository.DeleteItemAsync(id);
            return NoContent();
        }
    }
        
}
