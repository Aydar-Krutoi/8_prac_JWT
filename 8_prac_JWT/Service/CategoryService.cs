using _8_prac_JWT.DatabaseContext;
using _8_prac_JWT.Interfaces;
using _8_prac_JWT.Models;
using _8_prac_JWT.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _8_prac_JWT.Service
{
    public class CategoryService : ICategoryInterfaces
    {
        private readonly ContextDb _context;

        public CategoryService(ContextDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetAllCategoryAsync()
        {
            var categories = await _context.Categories.ToListAsync();

            if (categories.Count == 0)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет категорий"
                });
            }

            return new OkObjectResult(new
            {
                data = new { categories = categories },
                status = true
            });
        }

        public async Task<IActionResult> PostNewCategoryAsync(PostCategoryRequest postCategory)
        {
            if (string.IsNullOrEmpty(postCategory.Category_name))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Название категории не может быть пустым"
                });
            }

           

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Category_name.ToLower() == postCategory.Category_name.ToLower());

            if (category != null)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Уже есть категория с таким названием"
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.User_id == postCategory.User_id);


            if (user == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = $"Нет такой пользователя с таким id: {postCategory.User_id}"
                });
            }

            var category_ = new Category()
            {
                Category_name = postCategory.Category_name
            };

            var log = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = postCategory.User_id,
                Action_id = 6 // Добавление категории
            };

            await _context.AddAsync(log);
            await _context.AddAsync(category_);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
                , message = "Успешно создана"
            });
        }

        public async Task<IActionResult> PutCategoryAsync(PutCategoryRequest putCategory)
        {
            if (putCategory.Id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Неккоректный Id"
                });
            }

            var category = await _context.Categories.FirstOrDefaultAsync(b => b.Category_id == putCategory.Id);

            

            if (category == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = $"Нет такой категории с таким id: {putCategory.Id}"
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.User_id == putCategory.User_id);


            if (user == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = $"Нет такой пользователя с таким id: {putCategory.Id}"
                });
            }

            if (string.IsNullOrEmpty(putCategory.Category_name))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Название категории не может быть пустым"
                });
            }

            category.Category_name = putCategory.Category_name;

            var log = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = user.User_id,
                Action_id = 7 // Изменение категории
            };

            await _context.AddAsync(log);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Успешно"
            });
        }

        public async Task<IActionResult> DeleteCategoryAsync(DeletedCategoryRequest deletedCategory)
        {
            if (deletedCategory.Id_Cat == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Неккореткный Id"
                });
            }



            var category = await _context.Categories.FirstOrDefaultAsync(p => p.Category_id == deletedCategory.Id_Cat);

            if (category == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такой категории с таким id"
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.User_id == deletedCategory.User_id);


            if (user == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = $"Нет такой пользователя с таким id: {deletedCategory.User_id}"
                });
            }

            _context.Categories.Remove(category);

            var log = new LogUserAction()
            {
                Created_at = DateTime.Now,
                User_id = deletedCategory.User_id,
                Action_id = 8 // категория удаления
            };

            await _context.AddAsync(log);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Успешно"
            });
        }
    }
}
