using _8_prac_JWT.DatabaseContext;
using _8_prac_JWT.Interfaces;
using _8_prac_JWT.Models;
using _8_prac_JWT.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _8_prac_JWT.Service
{
    public class OrderService : IOrderInterfaces
    {
        private readonly ContextDb _context;
        public OrderService(ContextDb context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> PostNewOrderAsync(PostNewOrder postNewOrder)
        {
            if (postNewOrder.delivery_type_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Необходимо указать тип доставки"
                });
            }

            var delivery_type = await _context.Deliveries.FirstOrDefaultAsync(dt => dt.Delivery_id == postNewOrder.delivery_type_id);

            if (delivery_type == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого типа доставки с таким id"
                });
            }

            if (postNewOrder.payment_type_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Необходимо указать тип оплаты"
                });
            }

            var existing_payment_type = await _context.Payments.FirstOrDefaultAsync(dt => dt.Payment_id == postNewOrder.payment_type_id);

            if (existing_payment_type == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого типа оплаты с таким id"
                });
            }

            if (postNewOrder.user_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Необходимо указать пользователя"
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(dt => dt.User_id == postNewOrder.user_id);

            if (user == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого пользователя с таким id"
                });
            }

            var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.User_id == postNewOrder.user_id && b.Order_id == null);

            if (basket == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "В корзине нет товаров, чтобы оформить заказ"
                });
            }

            var existing_items_basket = await _context.BasketItems.FirstOrDefaultAsync(bi => bi.Basket_id == basket.Basket_id);

            if (existing_items_basket == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "В корзине нет товаров, чтобы оформить заказ"
                });
            }

            var order = new Order()
            {
                Order_date = DateOnly.FromDateTime(DateTime.Now),
                Total_amount = basket.Result_price,
                Status_id = 1,
                User_id = postNewOrder.user_id,
                Delivery_id = postNewOrder.delivery_type_id,
                Payment_id = postNewOrder.payment_type_id,
            };

            var log = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = postNewOrder.user_id,
                Action_id = 14 // создание заказа
            };

            await _context.AddAsync(log);
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();

            basket.Order_id = order.Order_id;

            var basket_ = new Basket()
            {
                Result_price = 0,
                User_id = postNewOrder.user_id,
                Order_id = null
            };

            await _context.AddAsync(basket_);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        // просмотр заказов пользователем
        public async Task<IActionResult> GetOrdersByUserIdAsync(GetOrdersByUserIdAsyncRequest getOrdersGetOrdersByUser)
        {
            if (getOrdersGetOrdersByUser.Id_user == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблемы с Id"
                });
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.User_id == getOrdersGetOrdersByUser.Id_user);

            if (existingUser == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого пользователя с таким id"
                });
            }

            var orders = _context.Orders.Where(o => o.User_id == getOrdersGetOrdersByUser.Id_user);

            if (orders.Count() == 0)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "У этого пользователя нет заказов"
                });
            }


            return new OkObjectResult(new
            {
                data = new { orders = orders.Include(o => o.Status) },
                status = true
            });
        }
    }
}
