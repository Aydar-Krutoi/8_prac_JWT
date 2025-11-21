using _8_prac_JWT.CustomAttributes;
using _8_prac_JWT.Interfaces;
using _8_prac_JWT.Requests;
using _8_prac_JWT.Service;
using Microsoft.AspNetCore.Mvc;

namespace _8_prac_JWT.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryInterfaces _categoryService;

        public CategoryController(ICategoryInterfaces categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        [RoleAuthorized([1])]
        public async Task<IActionResult> GetAllCategoryAsync()
        {
            return await _categoryService.GetAllCategoryAsync();
        }

        [HttpPost]
        [Route("CreateNewCategory")]
        [RoleAuthorized([1])]
        public async Task<IActionResult> PostNewCategoryAsync([FromBody]PostCategoryRequest postCategoryRequest)
        {
            return await _categoryService.PostNewCategoryAsync(postCategoryRequest);
        }

        [HttpPut]
        [Route("UpdateCategory")]
        [RoleAuthorized([1])]
        public async Task<IActionResult> PutCategoryAsync([FromBody] PutCategoryRequest putCategory)
        {
            return await _categoryService.PutCategoryAsync(putCategory);
        }

        [HttpDelete]
        [Route("DeleteCategory")]
        [RoleAuthorized([1])]
        public async Task<IActionResult> DeleteCategoryAsync([FromBody] DeletedCategoryRequest deletedCategory)
        {
            return await _categoryService.DeleteCategoryAsync(deletedCategory);
        }
        
    }
}
