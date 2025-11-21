using _8_prac_JWT.CustomAttributes;
using _8_prac_JWT.Interfaces;
using _8_prac_JWT.Requests;
using _8_prac_JWT.Service;
using Microsoft.AspNetCore.Mvc;

namespace _8_prac_JWT.Controllers
{
    public class BasketController : ControllerBase
    {
        private readonly IBasketInterfaces _basketService;

        public BasketController(IBasketInterfaces basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        [Route("CreateBasket")]
        [RoleAuthorized([3])]
        public async Task<IActionResult> PostNewProductForBasketAsync([FromBody]PostNewProdForBasket postNewProdForBasket)
        {
            return await _basketService.PostNewProductForBasketAsync(postNewProdForBasket);
        }

        [HttpDelete]
        [Route("DeleteProdutBasket")]
        [RoleAuthorized([3])]
        public async Task<IActionResult> DeleteProdcutFromBasketAsync([FromBody] DeleteProductFromBasket postNewProductFromBasket)
        {
            return await _basketService.DeleteProdcutFromBasketAsync(postNewProductFromBasket);
        }
        
    }
}
