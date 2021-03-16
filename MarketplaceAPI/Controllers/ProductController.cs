using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceAPI.Model;
using MarketplaceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController :ControllerBase
    {
        private IProductService _productService;

        public ProductController( IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Route("{categoryId}")]

        public async Task<ActionResult<IList<Product>>> GetAllProductsFromCategoryAsync([FromRoute] int categoryId)
        {
            try
            {
                var products = await _productService.GetAllProductsFromCategoryAsync(categoryId);
              
                return Ok(products);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("product_to_cart/{customerUsername}")]
        public async Task<ActionResult> AddProductToCartAsync([FromBody] Product product,[FromRoute] string customerUsername)
        {
            try
            {
                await _productService.AddProductToCartAsync(product,customerUsername);
                return Ok(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
           
        }
    }
}