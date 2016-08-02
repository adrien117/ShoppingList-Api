using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopListAPI.Core;
using ShopListAPI.Core.Models;
using ShopListAPI.Core.Repositories;
using ShopListAPI.Persistence.Repository;

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