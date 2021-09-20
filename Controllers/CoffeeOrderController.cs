using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tqviet.CoffeeShop.Models;
using Tqviet.CoffeeShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Tqviet.CoffeeShop.Validator;
using Newtonsoft.Json;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tqviet.CoffeeShop.Controllers
{
    public class Result
    {
        public string message { get; set; }
        public string prepared { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeOrderController : ControllerBase
    {
        private readonly CoffeeOrderDbContext _dbContext;
        private IHttpContextAccessor _accessor;
        private ICoffeeOrderService _coffeeOrderService;
        public static int Counter = 1;
        //private readonly DataServices _dataServices;

        public CoffeeOrderController(IHttpContextAccessor accessor, CoffeeOrderDbContext dbContext)
        {
            _accessor = accessor;
            _dbContext = dbContext;
        }
        /*
        public CoffeeOrderController(ICoffeeOrderService coffeeOrderService)
        {
            _coffeeOrderService = coffeeOrderService;
        }*/
        // GET: api/<CoffeeOrderController>
        /// <summary>
        /// Returns list of coffee order
        /// </summary>
        /*[HttpGet("coffee-order-list")]
        public async ValueTask<List<CoffeeOrders>> Get()
        {
            return (await _dbContext.CoffeeOrders.ToListAsync());
        }*/
        [HttpGet("coffee-order-list")]
        [ProducesResponseType(typeof(List<CoffeeOrders>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async ValueTask<IActionResult> Get()
        {
            List<CoffeeOrders> orders = await _dbContext.CoffeeOrders.ToListAsync();
            return StatusCode(200, orders);
        }

        /// <summary>
        /// Returns coffee order by id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CoffeeOrders), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async ValueTask<IActionResult> Get(int id)
        {
            CoffeeOrders order = await _dbContext.CoffeeOrders.FindAsync(id);
            if (order == null)// Not Found
            {
                return StatusCode(400,"Not found");
            }
            return StatusCode(200, order);
        }

        /// <summary>
        /// Brew-coffee
        /// </summary>
        [HttpGet("brew-coffee")]
        [ProducesResponseType(typeof(JsonResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(503)]
        [ProducesResponseType(typeof(string),418)]
        public async Task<IActionResult> GetCoffeeNumber()
        {
            if (Counter < 5)
            {
                //Check for 1/4
                if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
                {
                    return StatusCode(418,"");
                }
                float temp = await Temperature();
                Counter++;
                if (temp>30) 
                {
                    Result resultHot = new Result
                    {
                        message = "Your refreshing iced coffee is ready",
                        prepared = System.DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH\\:mm\\:ss zzff")
                    };
                    return StatusCode(200, resultHot);
                }
                Result result = new Result
                {
                    message = "Your piping hot coffee is ready",
                    prepared = System.DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH\\:mm\\:ss zzff")
                };
                return StatusCode(200, result);
            }
            else
            {
                Counter = 1;
                return StatusCode(503, "Out of service");
            }
            
        }

        /// <summary>
        /// Order new coffee
        /// </summary>
        [HttpPost("coffee-order-new")]
        [ProducesResponseType(typeof(CoffeeOrders), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async ValueTask<IActionResult> Post([FromBody] CoffeeOrders order)
        {
            // Check CoffeeOrdersValidator
            CoffeeOrdersValidator validator = new CoffeeOrdersValidator();
            await validator.ValidateAsync(order);//await ModelValidation.ValidateModel<CoffeeOrders, CoffeeOrdersValidator>(order, true);
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Giá trị nhập vào không hợp lệ");
            }
            CoffeeOrders nOrder = new CoffeeOrders();
            nOrder.CoffeeOrderClientIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            nOrder.CoffeeOrderDateTime = DateTime.Now;
            nOrder.CoffeeOrderQuantity = order.CoffeeOrderQuantity;
            await _dbContext.AddAsync(nOrder);
            _dbContext.SaveChanges();
            Counter++;
            return StatusCode(200, nOrder);

        }

        // PUT api/<CoffeeOrderController>/5
        [HttpPut("coffee-order-update")]
        [ProducesResponseType(typeof(CoffeeOrders), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async ValueTask<IActionResult> Put(int id, [FromBody] CoffeeOrders order)
        {
            // Check CoffeeOrdersValidator
            CoffeeOrdersValidator validator = new CoffeeOrdersValidator();
            await validator.ValidateAsync(order);//await ModelValidation.ValidateModel<CoffeeOrders, CoffeeOrdersValidator>(order, true);
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Giá trị nhập vào không hợp lệ");
            }
            CoffeeOrders nOrder = new CoffeeOrders();
            nOrder.CoffeeOrderClientIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            nOrder.CoffeeOrderDateTime = DateTime.Now;
            nOrder.CoffeeOrderQuantity = order.CoffeeOrderQuantity;
            _dbContext.Entry(order).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            Counter++;
            return StatusCode(200, nOrder);
        }

        // DELETE api/<CoffeeOrderController>/5
        [HttpDelete("delete-order")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async ValueTask<IActionResult> Delete(int id)
        {
            try
            {
                CoffeeOrders order = await _dbContext.CoffeeOrders.FindAsync(id);
                if (order == null)
                {
                    return StatusCode(200, "Not found order");
                }
                _dbContext.CoffeeOrders.Remove(order);
                await _dbContext.SaveChangesAsync();
                return StatusCode(200, "Order Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(503, "Error " + e.Message);
            }
        }
        
        // GET Temperature of a City
        [HttpGet("temperature-City")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async ValueTask<IActionResult> City(string city)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q=DaNang&appid=e7d5f9c9d0500ba25b31448cfd875c5e");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);
                    return Ok(new
                    {
                        Temp = rawWeather.Main.Temp,
                        Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
                        City = rawWeather.Name
                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }

        // GET Temperature of a City
        [HttpGet("temperature")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async ValueTask<float> Temperature()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q=DaNang&appid=e7d5f9c9d0500ba25b31448cfd875c5e");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);
                    return (float.Parse(rawWeather.Main.Temp))/10;
                }
                catch (HttpRequestException httpRequestException)
                {
                    return -273;
                    //throw BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }
    }
}
