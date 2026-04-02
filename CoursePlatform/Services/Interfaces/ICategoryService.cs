using CoursePlatform.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoursePlatform.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryViewModel>> GetAllAsync();
    Task<List<SelectListItem>> GetSelectListAsync();

    Task<CategoryViewModel?> GetByIdAsync(int id);
    Task CreateAsync(CategoryViewModel model);
    Task UpdateAsync(CategoryViewModel model);
    Task DeleteAsync(int id);
}