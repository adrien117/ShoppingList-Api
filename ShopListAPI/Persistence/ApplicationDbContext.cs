using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using ShopListAPI.Core.Models;

namespace ShopListAPI.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ShopItem> ShopItems { get; set; }

        public override int SaveChanges()
        {
            AddVersionStamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            AddVersionStamps();
            return base.SaveChangesAsync();
        }

        private void AddVersionStamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
                ? HttpContext.Current.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity) entity.Entity).CreatedBy = currentUsername;
                    ((BaseEntity) entity.Entity).CreatedDate = DateTime.UtcNow;
                    ((BaseEntity) entity.Entity).ModifiedDate = DateTime.UtcNow;

                }
                else
                {
                    ((BaseEntity)entity.Entity).ModifidedBy = currentUsername;
                    ((BaseEntity)entity.Entity).ModifiedDate = DateTime.UtcNow;
                }
            }
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}