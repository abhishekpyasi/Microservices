using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Common;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ItemsController : ControllerBase
    {

        private readonly IRepository<InventoryItem> itemsRepository;

        public ItemsController(IRepository<InventoryItem> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(Guid UserID)
        {

            if (UserID == Guid.Empty)
            {

                return BadRequest();

            }

            var items = (await itemsRepository.GetAllAsync(item => item.UserId == UserID)).
            Select(items => items.AsDto());

            return Ok(items);


        }
        [HttpPost]
        public async Task<ActionResult> PostAsync(GrantItemsDto grantItemsDto)
        {

            var inventoryItem = await itemsRepository.GetItemAsync(item => item.UserId == grantItemsDto.UserId

            && item.CatalogItemID == grantItemsDto.CatalogItemID);

            if (inventoryItem == null)
            {

                inventoryItem = new InventoryItem
                {

                    CatalogItemID = grantItemsDto.CatalogItemID,
                    UserId = grantItemsDto.UserId,
                    Quantity = grantItemsDto.Quantity,
                    AcquiredDate = DateTimeOffset.UtcNow,
                };

                await itemsRepository.CreateAsync(inventoryItem);

            }

            else
            {

                inventoryItem.Quantity = inventoryItem.Quantity + grantItemsDto.Quantity;
                await itemsRepository.UpdateAsync(inventoryItem);
            }
            return Ok();
        }



    }
}