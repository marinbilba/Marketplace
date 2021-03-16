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
    public class CategoryController :ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IList<Category>>> GetAllCategoriesAsync()
        {
            try
            {
                var products = await _categoryService.GetAllCategoriesAsync();
              
                return Ok(products);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}