using Microsoft.AspNetCore.Mvc;

namespace _8_prac_JWT.Interfaces
{
    public interface IProductInterfaces
    {
        Task<IActionResult> GetAllProductAsync();
    }
}
