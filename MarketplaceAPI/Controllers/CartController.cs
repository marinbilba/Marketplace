using System;
using System.Threading.Tasks;
using MarketplaceAPI.Model;
using MarketplaceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceAPI.Controllers
{    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpDelete]
        [Route("product/{cartId}/{productId}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int cartId,int productId)
        {
            try
            {
                _cartService.DeleteProduct(cartId,productId);
                return Ok();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost]
        [Route("placeOrder")]
        public async Task<ActionResult> PlaceOrder([FromBody] CustomerOrder order)
        {
            try
            {
                _cartService.PlaceOrder(order);
                return Ok();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
       
    }
}