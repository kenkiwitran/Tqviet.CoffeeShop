using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tqviet.CoffeeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace Tqviet.CoffeeShop.Services
{
    public class CoffeeOrderService: ICoffeeOrderService
    {
        public readonly CoffeeOrderDbContext _dbContext;

        public CoffeeOrderService(CoffeeOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<string> GetCoffeeOrderbyId(int Id)
        {
            var name = await _dbContext.CoffeeOrders.Where(c => c.CoffeeOrderId == Id).Select(d => d.CoffeeOrderClientIp).FirstOrDefaultAsync();
            return name.ToString();
        }

        public async ValueTask<CoffeeOrders> GetCoffeeOrderDetails(int Id)
        {
            var emp = await _dbContext.CoffeeOrders.FirstOrDefaultAsync(c => c.CoffeeOrderId == Id);
            return emp;
        }

    }
}
