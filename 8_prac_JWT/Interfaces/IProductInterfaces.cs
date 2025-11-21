using _8_prac_JWT.Requests;
using Microsoft.AspNetCore.Mvc;

namespace _8_prac_JWT.Interfaces
{
    public interface IProductInterfaces
    {
        Task<IActionResult> GetAllProductAsync(GetAllProductRequest getAllProduct);
        Task<IActionResult> PostNewProductAsync(PostNewProductRequest postNewProduct);
        Task<IActionResult> PutProductAsync(PutProductRequest putProduct);
        Task<IActionResult> DeleteProductAsync(DeleteProductRequest deleteProduct);
        Task<IActionResult> Top10ProductsAsync();
    }
}
