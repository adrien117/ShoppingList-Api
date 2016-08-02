using ShopListAPI.Core.Repositories;

namespace ShopListAPI.Core
{
    public interface IUnitOfWork
    {
        IShopItemRepository ShopItems { get; }
        void Complete();
    }
}