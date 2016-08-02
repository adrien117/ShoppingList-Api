using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopListAPI.Models;
using ShopListAPI.Repository;

namespace ShopListAPI.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IShopItemRepository ShopItems { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ShopItems = new ShopItemRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}