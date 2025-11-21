using _8_prac_JWT.DatabaseContext;
using _8_prac_JWT.Interfaces;
using _8_prac_JWT.Models;
using _8_prac_JWT.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _8_prac_JWT.Service
{
    public class BasketService : IBasketInterfaces
    {
        private readonly ContextDb _context;
        private readonly IHttpContextAccessor _httpContext;
        public BasketService(ContextDb context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
        public async Task<IActionResult> PostNewProductForBasketAsync(PostNewProdForBasket postNewBasket)
        {
            string? token = _httpContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (token == null)
            {
                return new BadRequestObjectResult(new { status = false, message = "Неправильный jwt" });
            }
            var userid = await _context.Sessions.FirstOrDefaultAsync(u => u.Token == token);
            if (userid == null)
            {
                return new NotFoundObjectResult(new { status = false, message = "Ошибка" });
            }

            int customerId = userid.User_id;



            var user = await _context.Users.FirstOrDefaultAsync(u => u.User_id == customerId);

            if (user == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого пользователя с таким ИД"
                });
            }

            if (postNewBasket.Quantity <= 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Неккоректное значение у <Количество>"
                });
            }

            if (postNewBasket.Product_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "У корзины должен быть Продукт ИД"
                });
            }

            var product = await _context.Products.FirstOrDefaultAsync(u => u.Product_id == postNewBasket.Product_id);

            if (product == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого товара с таким ИД"
                });
            }

            if (product.Stock == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Товара нет в наличии"
                });
            }

            var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.User_id == customerId && b.Order_id == null);

            if (basket == null)
            {
                var basket_ = new Basket()
                {
                    Result_price = 0,
                    User_id = customerId,
                    Order_id = null
                };
                await _context.AddAsync(basket_);
            }
            await _context.SaveChangesAsync();

            var our_basket = await _context.Baskets.FirstOrDefaultAsync(b => b.User_id == customerId && b.Order_id == null);

            var product_in_basket = await _context.BasketItems.FirstOrDefaultAsync(bi => bi.Product_id == postNewBasket.Product_id && bi.Basket_id == our_basket.Basket_id);

            if (product_in_basket == null)
            {
                var basket_item = new BasketItem()
                {
                    Quantity = postNewBasket.Quantity,
                    Basket_id = our_basket.Basket_id,
                    Product_id = postNewBasket.Product_id
                };
                await _context.AddAsync(basket_item);
            }
            else
            {
                product_in_basket.Quantity += postNewBasket.Quantity;
            }

            our_basket.Result_price += Math.Round(product.Price * postNewBasket.Quantity, 2);
            if (our_basket.Result_price > 0 && our_basket.Result_price < 1)
            {
                our_basket.Result_price = 0.00;
            }

            product.Stock -= postNewBasket.Quantity;

            var log = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = customerId,
                Action_id = 12 // добавления продукта в корзину
            };

            await _context.AddAsync(log);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<IActionResult> DeleteProdcutFromBasketAsync(DeleteProductFromBasket deleteProductFromBasket)
        {
            string? token = _httpContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (token == null)
            {
                return new BadRequestObjectResult(new { status = false, message = "Неправильный jwt" });
            }
            var userid = await _context.Sessions.FirstOrDefaultAsync(u => u.Token == token);
            if (userid == null)
            {
                return new NotFoundObjectResult(new { status = false, message = "Ошибка" });
            }

            int customerId = userid.User_id;

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Product_id == deleteProductFromBasket.product_id);

            if (product == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого продукта с таким id"
                });
            }

            if (customerId == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Отсутствует user_id"
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(p => p.User_id == customerId);

            if (user == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого пользователя с таким id"
                });
            }

            var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.User_id == customerId && b.Order_id == null);

            if (basket == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "У пользователя нет заказанных товаров"
                });
            }

            var product_in_basket_items = await _context.BasketItems.FirstOrDefaultAsync(bi => bi.Product_id == deleteProductFromBasket.product_id && bi.Basket_id == basket.Basket_id);

            if (product_in_basket_items == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Такого товара нет в корзине"
                });
            }

            _context.BasketItems.Remove(product_in_basket_items);
            basket.Result_price -= Math.Round(product_in_basket_items.Product.Price * product_in_basket_items.Quantity, 2);

            product.Stock += product_in_basket_items.Quantity;

            var log = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = customerId,
                Action_id = 13 // удаление товара из корзины
            };

            await _context.AddAsync(log);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });

        }
    }
}   
