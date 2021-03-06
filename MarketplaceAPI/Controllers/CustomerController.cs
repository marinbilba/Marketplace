using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceAPI.Model;
using MarketplaceAPI.Services;
using MarketplaceAPI.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService1)
        {
            _customerService = customerService1;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] Customer customer)
        {
            try
            {
                await _customerService.LoginAsync(customer);
                return Ok(customer);
            }
            catch (IncorrectUsernameOrPassword ex)
            {
                Console.WriteLine(ex);
                return StatusCode(403);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
           
        }
        [HttpGet]
        [Route("cart/{customerUsername}")]
        public async Task<ActionResult<Cart>> GetCustomerCartAsync([FromRoute] string customerUsername)
        {
            try
            {
                var cart = await _customerService.GetCustomerCartAsync(customerUsername);
              
                return Ok(cart);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet]
        [Route("order_history/{customerUsername}")]
        public async Task<ActionResult<IList<CustomerOrder>>> GetCustomerOrderHistory([FromRoute] string customerUsername)
        {
            try
            {
                IList<CustomerOrder> customerOrders = await _customerService.GetCustomerOrderHistoryAsync(customerUsername);
              
                return Ok(customerOrders);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
       
    }
}