using _8_prac_JWT.Requests;
using Microsoft.AspNetCore.Mvc;

namespace _8_prac_JWT.Interfaces
{
    public interface IBasketInterfaces
    {
        Task<IActionResult> PostNewProductForBasketAsync(PostNewProdForBasket postNewProdForBasket);
        Task<IActionResult> DeleteProdcutFromBasketAsync(DeleteProductFromBasket postNewProductFromBasket);
    }
}
