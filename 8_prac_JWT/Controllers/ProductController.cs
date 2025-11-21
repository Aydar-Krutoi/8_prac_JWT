using _8_prac_JWT.CustomAttributes;
using _8_prac_JWT.Interfaces;
using _8_prac_JWT.Requests;
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
        [RoleAuthorized([1,2,3])]
        public async Task<IActionResult> GetAllProductAsync([FromBody]GetAllProductRequest getAllProduct)
        {
            return await _productService.GetAllProductAsync(getAllProduct);
        }

        [HttpPost]
        [Route("NewProduct")]
        [RoleAuthorized([1, 2])]
        public async Task<IActionResult> PostNewProductAsync([FromBody] PostNewProductRequest postNewProduct)
        {
            return await _productService.PostNewProductAsync(postNewProduct);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        [RoleAuthorized([1, 2])]
        public async Task<IActionResult> PutProductAsync([FromBody] PutProductRequest putProduct)
        {
            return await _productService.PutProductAsync(putProduct);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        [RoleAuthorized([1, 2])]
        public async Task<IActionResult> DeleteProductAsync([FromBody] DeleteProductRequest deleteProduct)
        {
            return await _productService.DeleteProductAsync(deleteProduct);
        }

        [HttpGet]
        [Route("Top10Product")]
        [RoleAuthorized([2])]
        public async Task<IActionResult> Top10ProductsAsync()
        {
            return await _productService.Top10ProductsAsync();
        }
    }
}
