using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopListAPI.Dtos;

namespace ShopListAPI.Controllers.Api
{
    public class ShoppingListController : ApiController
    {
        [HttpGet]
        public IEnumerable<ShopItemDto> GetAllShopItems() 
        {
            List<ShopItemDto> items;
            items = new List<ShopItemDto>();
            //TODO: get the list from the repo->database...
            return items;
        }

        [HttpGet]
        public ShopItemDto GetShopItemById(string id) 
        {
            //TODO: get a specific shopping list item... base on the id provided...
            var item = new ShopItemDto
            {
                ItemName = "pepsi",
                ItemQuantity = 1
            };
            return item;
        }

        [HttpPost]
        public IHttpActionResult AddShopItem(ShopItemDto item)
        {
            //TODO: add the item passed via parameter to the repo...
            return Ok();
        } // add an item to the shopping list...

        [HttpPut]
        public IHttpActionResult FullUpdateShopItem(string itemId, ShopItemDto item)
        {
            //TODO: Insert the whole Item....
            return Ok(item);
        }

        [HttpPatch]
        public IHttpActionResult DeltaUpdateShopItem(string itemId, ShopItemDto item)
        {
            //TODO: Insert the partial Item....
            return Ok(item);
        }

        [HttpDelete]
        public IHttpActionResult DeleteShopItem(string id)
        {
            //TODO: delete shop item from repo...
            return NotFound();
        }
    }
}
