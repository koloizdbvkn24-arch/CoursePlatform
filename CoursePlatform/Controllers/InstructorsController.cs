using CoursePlatform.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoursePlatform.Controllers;

public class InstructorsController : Controller
{
    private readonly IInstructorService _instructorService;

    public InstructorsController(IInstructorService instructorService)
    {
        _instructorService = instructorService;
    }

    public async Task<IActionResult> Index()
    {
        var instructors = await _instructorService.GetAllAsync();
        return View(instructors);
    }
}