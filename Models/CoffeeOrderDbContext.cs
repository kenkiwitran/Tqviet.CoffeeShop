using Microsoft.EntityFrameworkCore;

namespace Tqviet.CoffeeShop.Models
{
    public class CoffeeOrderDbContext:DbContext
    {
        public CoffeeOrderDbContext() : base() { }
        public CoffeeOrderDbContext(DbContextOptions options): base(options) { }

        public DbSet<CoffeeOrders> CoffeeOrders { get; set; }
    }
}
