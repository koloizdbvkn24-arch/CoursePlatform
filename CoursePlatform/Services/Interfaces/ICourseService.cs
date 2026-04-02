using CoursePlatform.ViewModels.Courses;

namespace CoursePlatform.Services.Interfaces;

public interface ICourseService
{
    Task<List<CourseListItemViewModel>> GetAllAsync();
    Task<CourseDetailsViewModel?> GetByIdAsync(int id);

    Task<CreateCourseViewModel> GetCreateViewModelAsync();
    Task<EditCourseViewModel?> GetEditViewModelAsync(int id);

    Task CreateAsync(CreateCourseViewModel model);
    Task UpdateAsync(EditCourseViewModel model);
    Task DeleteAsync(int id);
}