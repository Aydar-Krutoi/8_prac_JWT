using _8_prac_JWT.CustomAttributes;
using _8_prac_JWT.Interfaces;
using _8_prac_JWT.Requests;
using _8_prac_JWT.Service;
using Microsoft.AspNetCore.Mvc;

namespace _8_prac_JWT.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderInterfaces _orderService;

        public OrderController(IOrderInterfaces orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("GetOrdersByUserId")]
        [RoleAuthorized([2])]
        public async Task<IActionResult> GetOrdersByUserIdAsync([FromBody]GetOrdersByUserIdAsyncRequest getOrdersByUserIdAsync)
        {
            return await _orderService.GetOrdersByUserIdAsync(getOrdersByUserIdAsync);
        }

        [HttpPost]
        [Route("CreateOrder")]
        [RoleAuthorized([2])]
        public async Task<IActionResult> PostNewOrderAsync([FromBody] PostNewOrder postNewOrder)
        {
            return await _orderService.PostNewOrderAsync(postNewOrder);
        }
        
    }
}
