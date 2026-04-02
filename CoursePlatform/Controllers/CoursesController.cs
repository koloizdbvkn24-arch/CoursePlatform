using CoursePlatform.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoursePlatform.Controllers;

public class CoursesController : Controller
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _courseService.GetAllAsync();
        return View(courses);
    }

    public async Task<IActionResult> Details(int id)
    {
        var course = await _courseService.GetByIdAsync(id);

        if (course is null)
        {
            return NotFound();
        }

        return View(course);
    }
}