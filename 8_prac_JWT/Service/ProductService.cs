using _8_prac_JWT.DatabaseContext;
using _8_prac_JWT.Interfaces;
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

        public async Task<IActionResult> GetAllProductAsync()
        {
            var products = await _context.Products.ToListAsync();
            if(products.Count == 0)
            {
                return new NotFoundObjectResult(new
                {
                    message = "Товары не найдены",
                    status = false
                });
            }
            return new ObjectResult(new
            {
                data = new {products = products},
                status = true
            });
        }
    }
}
