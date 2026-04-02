using CoursePlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoursePlatform.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ICourseService _courseService;
    private readonly ICategoryService _categoryService;
    private readonly IInstructorService _instructorService;

    public AdminController(
        ICourseService courseService,
        ICategoryService categoryService,
        IInstructorService instructorService)
    {
        _courseService = courseService;
        _categoryService = categoryService;
        _instructorService = instructorService;
    }

    public IActionResult Dashboard()
    {
        return View();
    }

    public async Task<IActionResult> Courses()
    {
        var courses = await _courseService.GetAllAsync();
        return View(courses);
    }

    [HttpGet]
    public async Task<IActionResult> CreateCourse()
    {
        var model = await _courseService.GetCreateViewModelAsync();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CoursePlatform.ViewModels.Courses.CreateCourseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = await _categoryService.GetSelectListAsync();
            model.Instructors = await _instructorService.GetSelectListAsync();
            return View(model);
        }

        await _courseService.CreateAsync(model);
        return RedirectToAction(nameof(Courses));
    }

    [HttpGet]
    public async Task<IActionResult> EditCourse(int id)
    {
        var model = await _courseService.GetEditViewModelAsync(id);
        if (model is null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditCourse(CoursePlatform.ViewModels.Courses.EditCourseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = await _categoryService.GetSelectListAsync();
            model.Instructors = await _instructorService.GetSelectListAsync();
            return View(model);
        }

        await _courseService.UpdateAsync(model);
        return RedirectToAction(nameof(Courses));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        await _courseService.DeleteAsync(id);
        return RedirectToAction(nameof(Courses));
    }

    public async Task<IActionResult> Categories()
    {
        var categories = await _categoryService.GetAllAsync();
        return View(categories);
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View(new CoursePlatform.ViewModels.Categories.CategoryViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CoursePlatform.ViewModels.Categories.CategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _categoryService.CreateAsync(model);
        return RedirectToAction(nameof(Categories));
    }

    [HttpGet]
    public async Task<IActionResult> EditCategory(int id)
    {
        var model = await _categoryService.GetByIdAsync(id);
        if (model is null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(CoursePlatform.ViewModels.Categories.CategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _categoryService.UpdateAsync(model);
        return RedirectToAction(nameof(Categories));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteAsync(id);
        return RedirectToAction(nameof(Categories));
    }

    public async Task<IActionResult> Instructors()
    {
        var instructors = await _instructorService.GetAllAsync();
        return View(instructors);
    }

    [HttpGet]
    public IActionResult CreateInstructor()
    {
        return View(new CoursePlatform.ViewModels.Instructors.InstructorViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> CreateInstructor(CoursePlatform.ViewModels.Instructors.InstructorViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _instructorService.CreateAsync(model);
        return RedirectToAction(nameof(Instructors));
    }

    [HttpGet]
    public async Task<IActionResult> EditInstructor(int id)
    {
        var model = await _instructorService.GetByIdAsync(id);
        if (model is null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditInstructor(CoursePlatform.ViewModels.Instructors.InstructorViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _instructorService.UpdateAsync(model);
        return RedirectToAction(nameof(Instructors));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteInstructor(int id)
    {
        await _instructorService.DeleteAsync(id);
        return RedirectToAction(nameof(Instructors));
    }
}