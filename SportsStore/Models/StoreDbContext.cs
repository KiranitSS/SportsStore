using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) 
            : base(options) 
        {
        }

        public DbSet<Product> Products { get => this.Set<Product>(); }

        public DbSet<Order> Orders { get => this.Set<Order>(); }
    }
}
