using _8_prac_JWT.DatabaseContext;
using _8_prac_JWT.Interfaces;
using _8_prac_JWT.Models;
using _8_prac_JWT.Requests;
using _8_prac_JWT.UniversalMethod;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _8_prac_JWT.Service
{
    public class ProductService : IProductInterfaces
    {
        private readonly ContextDb _context;

        public ProductService(ContextDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetAllProductAsync(GetAllProductRequest getAllProduct)
        {
            List<Product> products;

            if (string.IsNullOrEmpty(getAllProduct.Filter_by_category))
            {
                products = await _context.Products.Include(p => p.Category)
                    .ToListAsync();
            }
            else
            {
                products = await _context.Products.Include(p => p.Category).Where(p => p.Category.Category_name.Contains(getAllProduct.Filter_by_category)).ToListAsync();
            }

            if (getAllProduct.Min_price < 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Минимальная цена не может быть отрицательной"
                });
            }

            if (getAllProduct.Max_price < 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Максимальная цена не может быть отрицательной"
                });
            }

            if (getAllProduct.Max_price > 0 && getAllProduct.Min_price > getAllProduct.Max_price)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Минимальная цена не может быть больше максимальной"
                });
            }

            if (getAllProduct.Min_price > 0)
            {
                products = products.Where(p => p.Price >= getAllProduct.Min_price).ToList();
            }

            if (getAllProduct.Max_price > 0)
            {
                products = products.Where(p => p.Price <= getAllProduct.Max_price).ToList();
            }

            if (getAllProduct.In_stock == true)
            {
                products = products.Where(p => p.Stock > 0).ToList();
            }
            else
            {
                products = products.Where(p => p.Stock == 0).ToList();
            }

            if (!string.IsNullOrEmpty(getAllProduct.Sort_by_date))
            {
                if (getAllProduct.Sort_by_date.ToLower() == "desc")
                {
                    products = products.OrderByDescending(p => p.Created_at).ToList();
                }
                else if (getAllProduct.Sort_by_date.ToLower() == "asc")
                {
                    products = products.OrderBy(p => p.Created_at).ToList();
                }
            }

            if (!string.IsNullOrEmpty(getAllProduct.Sort_by_price))
            {
                if (getAllProduct.Sort_by_price.ToLower() == "desc")
                {
                    products = products.OrderByDescending(p => p.Price).ToList();
                }
                else if (getAllProduct.Sort_by_price.ToLower() == "asc")
                {
                    products = products.OrderBy(p => p.Price).ToList();
                }
            }

            if (string.IsNullOrEmpty(getAllProduct.Sort_by_date) && string.IsNullOrEmpty(getAllProduct.Sort_by_price))
            {
                products = products.OrderBy(p => p.Product_name).ToList();
            }

            if (products.Count == 0)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет продуктов"
                });
            }

            return new OkObjectResult(new
            {
                data = new { products = products },
                status = true
            });
        }

        public async Task<IActionResult> PostNewProductAsync(PostNewProductRequest postNewPoduct)
        {
            if (string.IsNullOrEmpty(postNewPoduct.Product_name))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Название продукта не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(postNewPoduct.Description))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Описание продукта не может быть пустым"
                });
            }

            if (postNewPoduct.Price <= 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "У продукта должна быть цена,  положительная"
                });
            }

            if (postNewPoduct.Stock <= 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "У продукта должно быть кол-во на складе, и это положительное число"
                });
            }

            if (postNewPoduct.Category_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "У продукта должна быть категория"
                });
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Category_id == postNewPoduct.Category_id);

            if (category == null)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет такой категории с таким id"
                });
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Product_name.ToLower() == postNewPoduct.Product_name.ToLower());

            if (product != null)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Товар с таким названием уже существует"
                });
            }

            var product_ = new Product()
            {
                Product_name = postNewPoduct.Product_name,
                Description = postNewPoduct.Description,
                Price = postNewPoduct.Price,
                Created_at = DateOnly.FromDateTime(DateTime.Now),
                Is_active = postNewPoduct.Is_active,
                Stock = postNewPoduct.Stock,
                Category_id = postNewPoduct.Category_id
            };

            var log = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = postNewPoduct.User_id,
                Action_id = 9 // создание продукта 
            };

            await _context.AddAsync(product_);
            await _context.AddAsync(log);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
                , message = "Успешно"
            });
        }

        public async Task<IActionResult> PutProductAsync(PutProductRequest putProduct)
        {
            if (putProduct.ID == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Неккоретный Ид"
                });
            }

            var product = await _context.Products.FirstOrDefaultAsync(b => b.Product_id == putProduct.ID);

            if (product == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого продукта с таким Ид"
                });
            }

            if (string.IsNullOrEmpty(putProduct.Product_name))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Название продукта не может быть пустым"
                });
            }

            if (string.IsNullOrEmpty(putProduct.Description))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Описание продукта не может быть пустым"
                });
            }

            if (putProduct.Price <= 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "У продукта должна быть цена,  положительная"
                });
            }

            if (putProduct.Stock <= 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "У продукта должно быть кол-во на складе,  положительное число"
                });
            }

            if (putProduct.Category_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "У продукта должна быть категория"
                });
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Category_id == putProduct.Category_id);

            if (category == null)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет такой категории с таким id"
                });
            }

            product.Product_name = putProduct.Product_name;
            product.Description = putProduct.Description;
            product.Price = putProduct.Price;
            product.Stock = putProduct.Stock;
            product.Is_active = putProduct.Is_active;
            product.Category_id = putProduct.Category_id;

            var log = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = putProduct.User_id,
                Action_id = 10 // обновление продукта
            };
            await _context.AddAsync(log);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<IActionResult> DeleteProductAsync(DeleteProductRequest deleteProduct)
        {
            if (deleteProduct.ID_product == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Неккоректный с ИД"
                });
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Product_id == deleteProduct.ID_product);

            if (product == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого продукта с таким Ид"
                });
            }

            var log = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = deleteProduct.ID_user,
                Action_id = 11 // удаление продукта
            };

            await _context.AddAsync(log);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Успешно"
            });
        }

        public async Task<IActionResult> Top10ProductsAsync()
        {
            var topProducts = await _context.BasketItems.Include(bi => bi.Product).Include(bi => bi.Basket.Order).Where(bi => bi.Basket.Order.Status_id == 4).GroupBy(bi => bi.Product.Product_name)
            .Select(g => new
            {
                ProductName = g.Key,
                TimesSold = g.Count(),
                TotalRevenue = g.Sum(bi => bi.Product.Price)
            }).OrderByDescending(p => p.TimesSold).ToListAsync();

            if(topProducts.Count == 0)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет статистики"
                });
            }

            return new OkObjectResult(new
            {
                status = true,
                data = new { topProducts = topProducts }
            });
        }
    }
}
