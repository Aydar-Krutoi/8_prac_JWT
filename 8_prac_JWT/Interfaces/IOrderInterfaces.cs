using _8_prac_JWT.Requests;
using Microsoft.AspNetCore.Mvc;

namespace _8_prac_JWT.Interfaces
{
    public interface IOrderInterfaces
    {
        Task<IActionResult> GetOrdersByUserIdAsync(GetOrdersByUserIdAsyncRequest getOrdersByUserIdAsync);
        Task<IActionResult> PostNewOrderAsync(PostNewOrder postNewOrder);
    }
}
