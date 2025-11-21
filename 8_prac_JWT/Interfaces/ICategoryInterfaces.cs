using _8_prac_JWT.Requests;
using Microsoft.AspNetCore.Mvc;

namespace _8_prac_JWT.Interfaces
{
    public interface ICategoryInterfaces
    {
        Task<IActionResult> GetAllCategoryAsync();
        Task<IActionResult> PostNewCategoryAsync(PostCategoryRequest postCategoryRequest);
        Task<IActionResult> PutCategoryAsync(PutCategoryRequest putCategory);
        Task<IActionResult> DeleteCategoryAsync(DeletedCategoryRequest deletedCategory);
    }
}
