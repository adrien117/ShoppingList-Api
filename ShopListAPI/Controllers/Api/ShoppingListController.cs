using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopListAPI.Core;
using ShopListAPI.Core.Dtos;
using ShopListAPI.Persistence;

namespace ShopListAPI.Controllers.Api
{
    public class ShoppingListController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<ShopItemDto> GetAllShopItems() 
        {
            return _unitOfWork.ShopItems.GetAllShopItemsToDtos();
        }

        [HttpGet]
        public IHttpActionResult GetShopItemById(string id)
        {
            /* HumMMMMMMMM stupid mistake!!! caught by unit test :)
             * i was returning a model... i should be returning an http action
             * then i should return the correct result.... 200 or 404
             * unit test is always the best way of spoting errors....
            return _unitOfWork.ShopItems.GetShopItemDtoById(id);
            */
            ShopItemDto item = new ShopItemDto();
            item = _unitOfWork.ShopItems.GetShopItemDtoById(id);
            if (item.Name == "Adrien Was Here")
                return NotFound();
        
            return Ok(item);
        }

        [HttpPost]
        public IHttpActionResult AddShopItem(ShopItemDto item)
        {

            return Ok(_unitOfWork.ShopItems.AddShopItem(item));
        }

        [HttpPut]
        public IHttpActionResult FullUpdateShopItem(ShopItemDto item)
        {
            return Ok(_unitOfWork.ShopItems.FullUpdateShopItem(item));
        }

        [HttpPatch]
        public IHttpActionResult DeltaUpdateShopItem(ShopItemDto item)
        {
            //TODO: Insert the partial Item....
            return Ok(_unitOfWork.ShopItems.DeltaUpdateShopItem(item));
        }

        [HttpDelete]
        public IHttpActionResult DeleteShopItem(string id)
        {        
            _unitOfWork.ShopItems.DeleteSingleShopItem(id);
            return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult DeleteShopItemRecord(ShopItemDto item)
        {
            _unitOfWork.ShopItems.DeleteAllShopItem(item);
            return NotFound();
        }
        
    }
}
