using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ShopListAPI.Core.Dtos;
using ShopListAPI.Core.Models;
using ShopListAPI.Core.Repositories;

namespace ShopListAPI.Persistence.Repository
{
    public class ShopItemRepository : IShopItemRepository
    {
        // we need a context right?...
        private readonly ApplicationDbContext _context;

        public ShopItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Repository Methods

        public IEnumerable<ShopItem> GetAllShopItems()
        {
            return _context.ShopItems.ToList();
        }

        public IEnumerable<ShopItemDto> GetAllShopItemsToDtos()
        {
            Mapper.Initialize(c => c.CreateMap<ShopItem, ShopItemDto>());
            return _context.ShopItems.Select(Mapper.Map<ShopItem, ShopItemDto>); //AutoMapper magic <3 love it...
        }

        public ShopItemDto GetShopItemDtoById(string id)
        {
            IEnumerable<ShopItemDto> allDtoItem = new List<ShopItemDto>();
            allDtoItem = GetAllShopItemsToDtos();
            ShopItemDto foundItem = new ShopItemDto();
            // to avoid not reference error...
            foundItem.Name = "Adrien Was Here";

            foreach (var i in allDtoItem)
            {
                if (string.Compare(id, i.Name, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    foundItem = i;
                    return foundItem;
                }
            }
            return foundItem;
        }

        public ShopItemDto AddShopItem(ShopItemDto itemDto)
        {   // not sure if i can use automapper here...
            var name = itemDto.Name;
            var itemList = GetAllShopItems();
            ShopItem item = null;
            bool updated = false;
            ShopItemDto returnItemDto = new ShopItemDto();

            foreach (var i in itemList) // i know... i should be using a linq here... but i have a few things to take care of...
            {
                if (string.Compare(name, i.Name, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    returnItemDto = DeltaUpdateShopItem(itemDto);
                    updated = true;
                   
                    break;
                }
            }
            if (!updated)
            {
                item = new ShopItem
                {
                    Name = itemDto.Name,
                    CheckedOut = itemDto.CheckedOut,
                    Count = itemDto.Count,
                    UnitPrice = itemDto.UnitPrice,
                    TotalPrice = itemDto.UnitPrice * itemDto.Count
                };
                Mapper.Initialize(config => config.CreateMap<ShopItem, ShopItemDto>());
                Mapper.Map(item,returnItemDto);
                _context.ShopItems.Add(item);
                _context.SaveChanges();
            }

            return returnItemDto;
        }

        public ShopItemDto FullUpdateShopItem(ShopItemDto itemDto)
        {
            var currentItem = FindShopItemByName(itemDto.Name);
            using (_context)
            {
                var toBeUpdated = _context.ShopItems.Find(currentItem.Id);
                toBeUpdated.Count += itemDto.Count;
                toBeUpdated.UnitPrice = itemDto.UnitPrice;
                toBeUpdated.CheckedOut = itemDto.CheckedOut;
                toBeUpdated.TotalPrice = toBeUpdated.Count * currentItem.UnitPrice;
                _context.SaveChanges();
                currentItem = toBeUpdated;

            }
            //currentItem.Count += itemDto.Count;
            //currentItem.UnitPrice = itemDto.UnitPrice;
            //currentItem.CheckedOut = itemDto.CheckedOut;
            //currentItem.TotalPrice = currentItem.Count*currentItem.UnitPrice;

            //_context.ShopItems.Add(currentItem);
            ShopItemDto updatedItem = new ShopItemDto();
            Mapper.Initialize(config => config.CreateMap<ShopItem, ShopItemDto>());
            Mapper.Map(currentItem, updatedItem);
            return updatedItem;
        }

        public ShopItemDto DeltaUpdateShopItem(ShopItemDto itemDto)
        {
            var delatItem = FindShopItemByName(itemDto.Name);
            ShopItemDto returnItemDTO = new ShopItemDto();

            if (itemDto.Count > 0)
                delatItem.Count += itemDto.Count;
            if (itemDto.UnitPrice > 0)
                delatItem.UnitPrice = itemDto.UnitPrice;
            if (itemDto.CheckedOut != delatItem.CheckedOut)
                delatItem.CheckedOut = itemDto.CheckedOut;

            //recalculation of total price...
            delatItem.TotalPrice = delatItem.Count*delatItem.UnitPrice;

            using (_context)
            {
                var all = _context.ShopItems.Find(delatItem.Id);
                all.Count = delatItem.Count;
                all.CheckedOut = delatItem.CheckedOut;
                all.Name = delatItem.Name;
                all.TotalPrice = delatItem.TotalPrice;
                all.UnitPrice = delatItem.UnitPrice;
                all.Rating = delatItem.Rating;

                _context.SaveChanges();
            }
            // _context.ShopItems.Add(delatItem);
            // _context.SaveChanges();
            Mapper.Initialize(config => config.CreateMap<ShopItem, ShopItemDto>());
            Mapper.Map(delatItem, returnItemDTO);
            return returnItemDTO;
        }

        public void DeleteSingleShopItem(string name)
        {
            var itemToDelete = FindShopItemByName(name);       
            using (_context)
            {
               var x =  _context.ShopItems.Find(itemToDelete.Id);
                if (x.Count == 1)
                {
                    _context.ShopItems.Remove(x);
                }
                x.Count -= 1;
                x.TotalPrice = x.Count * x.UnitPrice;
                _context.SaveChanges();
            }
        }

        public void DeleteAllShopItem(ShopItemDto i) // the real id...
        {
            var x = FindShopItemByName(i.Name);
            using (_context)
            {
                var rowToBeDeleted = _context.ShopItems.Find(i.Name);
                _context.ShopItems.Remove(rowToBeDeleted);
                _context.SaveChanges();
            }
        }

        #endregion

        #region Helpers

        private ShopItem FindShopItemByName(string name)
        {
            var AllItem = GetAllShopItems();
            ShopItem foundItem = null;

            foreach (var f in AllItem)
            {
                if (string.Compare(name, f.Name, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    foundItem = f;
                    break;
                }
            }
            return foundItem;
        }
        #endregion



    }
}