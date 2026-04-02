using CoursePlatform.ViewModels.Instructors;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoursePlatform.Services.Interfaces;

public interface IInstructorService
{
    Task<List<InstructorViewModel>> GetAllAsync();
    Task<List<SelectListItem>> GetSelectListAsync();

    Task<InstructorViewModel?> GetByIdAsync(int id);
    Task CreateAsync(InstructorViewModel model);
    Task UpdateAsync(InstructorViewModel model);
    Task DeleteAsync(int id);
}