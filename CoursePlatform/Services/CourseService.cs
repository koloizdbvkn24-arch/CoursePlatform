using CoursePlatform.Data;
using CoursePlatform.Models.Entities;
using CoursePlatform.Services.Interfaces;
using CoursePlatform.ViewModels.Courses;
using CoursePlatform.ViewModels.Comments;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Services;

public class CourseService : ICourseService
{
    private readonly AppDbContext _context;
    private readonly ICategoryService _categoryService;
    private readonly IInstructorService _instructorService;

    public CourseService(
        AppDbContext context,
        ICategoryService categoryService,
        IInstructorService instructorService)
    {
        _context = context;
        _categoryService = categoryService;
        _instructorService = instructorService;
    }

    public async Task<List<CourseListItemViewModel>> GetAllAsync()
    {
        return await _context.Courses
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Instructor)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new CourseListItemViewModel
            {
                Id = x.Id,
                Title = x.Title,
                ShortDescription = x.Description.Length > 120
                    ? x.Description.Substring(0, 120) + "..."
                    : x.Description,
                Price = x.Price,
                DurationHours = x.DurationHours,
                Level = x.Level,
                CategoryName = x.Category.Name,
                InstructorName = x.Instructor.FullName
            })
            .ToListAsync();
    }

    public async Task<CourseDetailsViewModel?> GetByIdAsync(int id)
    {
        return await _context.Courses
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Instructor)
            .Include(x => x.Comments)
                .ThenInclude(x => x.User)
            .Where(x => x.Id == id)
            .Select(x => new CourseDetailsViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                DurationHours = x.DurationHours,
                Level = x.Level,
                CreatedAt = x.CreatedAt,
                CategoryName = x.Category.Name,
                InstructorName = x.Instructor.FullName,
                InstructorEmail = x.Instructor.Email,
                InstructorBio = x.Instructor.Bio,
                Comments = x.Comments
                    .OrderByDescending(c => c.CreatedAt)
                    .Select(c => new CommentItemViewModel
                    {
                        Id = c.Id,
                        Content = c.Content,
                        CreatedAt = c.CreatedAt,
                        Username = c.User.Username
                    })
                    .ToList(),
                NewComment = new CreateCommentViewModel
                {
                    CourseId = x.Id
                }
            })
            .FirstOrDefaultAsync();
    }

    public async Task<CreateCourseViewModel> GetCreateViewModelAsync()
    {
        return new CreateCourseViewModel
        {
            Categories = await _categoryService.GetSelectListAsync(),
            Instructors = await _instructorService.GetSelectListAsync()
        };
    }

    public async Task<EditCourseViewModel?> GetEditViewModelAsync(int id)
    {
        var course = await _context.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (course is null)
        {
            return null;
        }

        return new EditCourseViewModel
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            Price = course.Price,
            DurationHours = course.DurationHours,
            Level = course.Level,
            CategoryId = course.CategoryId,
            InstructorId = course.InstructorId,
            Categories = await _categoryService.GetSelectListAsync(),
            Instructors = await _instructorService.GetSelectListAsync()
        };
    }

    public async Task CreateAsync(CreateCourseViewModel model)
    {
        var course = new Course
        {
            Title = model.Title,
            Description = model.Description,
            Price = model.Price,
            DurationHours = model.DurationHours,
            Level = model.Level,
            CategoryId = model.CategoryId,
            InstructorId = model.InstructorId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EditCourseViewModel model)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == model.Id);

        if (course is null)
        {
            return;
        }

        course.Title = model.Title;
        course.Description = model.Description;
        course.Price = model.Price;
        course.DurationHours = model.DurationHours;
        course.Level = model.Level;
        course.CategoryId = model.CategoryId;
        course.InstructorId = model.InstructorId;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);

        if (course is null)
        {
            return;
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
    }
}