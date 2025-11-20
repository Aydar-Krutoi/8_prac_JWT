using _8_prac_JWT.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _8_prac_JWT.Controllers
{
    public class ProductController : ControllerBase
    {
        public readonly IProductInterfaces _productService;

        public ProductController(IProductInterfaces productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("AllProduct")]
        public async Task<IActionResult> GetAllProductAsync()
        {
            return await _productService.GetAllProductAsync();
        }
    }
}
