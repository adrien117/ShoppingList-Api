using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopListAPI.Dtos;
using ShopListAPI.Models;
using ShopListAPI.Persistence;
using ShopListAPI.Repository;

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
        public ShopItemDto GetShopItemById(string id)
        {
            return _unitOfWork.ShopItems.GetShopItemDtoById(id);
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
