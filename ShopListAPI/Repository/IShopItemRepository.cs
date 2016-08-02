using System.Collections.Generic;
using ShopListAPI.Dtos;
using ShopListAPI.Models;

namespace ShopListAPI.Repository
{
    public interface IShopItemRepository
    {
        ShopItemDto AddShopItem(ShopItemDto itemDto);
        void DeleteAllShopItem(ShopItemDto i);
        void DeleteSingleShopItem(string name);
        ShopItemDto DeltaUpdateShopItem(ShopItemDto itemDto);
        ShopItemDto FullUpdateShopItem(ShopItemDto itemDto);
        IEnumerable<ShopItem> GetAllShopItems();
        IEnumerable<ShopItemDto> GetAllShopItemsToDtos();
        ShopItemDto GetShopItemDtoById(string id);
    }
}