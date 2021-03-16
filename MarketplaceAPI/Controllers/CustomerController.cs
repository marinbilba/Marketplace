using System;
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
    }
}