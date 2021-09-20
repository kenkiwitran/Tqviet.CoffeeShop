using Moq;
using System;
using Xunit;
using Tqviet.CoffeeShop.Services;
using Tqviet.CoffeeShop.Controllers;
using Microsoft.AspNetCore.Mvc;
using Tqviet.CoffeeShop.Models;

namespace TestCoffeeShop
{
    public class CoffeeOrderTest
    {
        #region Property  
        public Mock<ICoffeeOrderService> mock = new Mock<ICoffeeOrderService>();
        #endregion

        [Fact]
        public async void GetCoffeeOrderbyId()
        {
            mock.Setup(p => p.GetCoffeeOrderbyId(2)).ReturnsAsync("::1");
            CoffeeOrderController order = new CoffeeOrderController(mock.Object);
            IActionResult result = await order.Get(1);
            Assert.Equal("::1", result.ToString());
        }
        [Fact]
        public async void GetCoffeeOrderDetails()
        {
            var employeeDTO = new CoffeeOrders()
            {
                CoffeeOrderId = 1,
                CoffeeOrderDateTime = DateTime.Now,
                CoffeeOrderClientIp = "192.168.1.1",
                CoffeeOrderQuantity = 1
            };
            mock.Setup(p => p.GetCoffeeOrderDetails(1)).ReturnsAsync(employeeDTO);
            CoffeeOrderController order = new CoffeeOrderController(mock.Object);
            var result = await order.Get(1);
            Assert.True(employeeDTO.Equals(result));
        }
    }
}
