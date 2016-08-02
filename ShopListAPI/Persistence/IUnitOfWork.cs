using ShopListAPI.Repository;

namespace ShopListAPI.Persistence
{
    public interface IUnitOfWork
    {
        IShopItemRepository ShopItems { get; }
        void Complete();
    }
}