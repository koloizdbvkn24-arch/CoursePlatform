using CoursePlatform.Data;
using CoursePlatform.Models.Entities;
using CoursePlatform.Services.Interfaces;
using CoursePlatform.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryViewModel>> GetAllAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            })
            .ToListAsync();
    }

    public async Task<List<SelectListItem>> GetSelectListAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToListAsync();
    }

    public async Task<CategoryViewModel?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            })
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(CategoryViewModel model)
    {
        var category = new Category
        {
            Name = model.Name,
            Description = model.Description
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CategoryViewModel model)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (category is null)
        {
            return;
        }

        category.Name = model.Name;
        category.Description = model.Description;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category is null)
        {
            return;
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}