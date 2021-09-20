using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tqviet.CoffeeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace Tqviet.CoffeeShop.Services
{
    public class DataServices
    {
        private CoffeeOrderDbContext _dbContext;

        public DataServices(CoffeeOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<List<CoffeeOrders>> GetCoffeeOrderListAsync()
        {
            List<CoffeeOrders> coffeeOrders = new List<CoffeeOrders>();
            coffeeOrders = await _dbContext.CoffeeOrders.ToListAsync();
            return coffeeOrders;
        }

        public async ValueTask<CoffeeOrders> GetCoffeeOrdersAsync(int Id)
        {
            return (await _dbContext.CoffeeOrders.FindAsync(Id));
        }

        public async ValueTask AddCoffeeOrderAsync(CoffeeOrders order)
        {
            _dbContext.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        public async ValueTask UpdateCoffeeOrders(CoffeeOrders order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }
    }
}
