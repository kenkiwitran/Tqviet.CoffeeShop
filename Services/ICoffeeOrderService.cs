using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tqviet.CoffeeShop.Models;

namespace Tqviet.CoffeeShop.Services
{
    public interface ICoffeeOrderService
    {
        ValueTask<string> GetCoffeeOrderbyId(int Id);
        ValueTask<CoffeeOrders> GetCoffeeOrderDetails(int Id);
    }
}
