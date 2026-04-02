using System.Security.Claims;
using CoursePlatform.Services.Interfaces;
using CoursePlatform.ViewModels.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoursePlatform.Controllers;

[Authorize]
public class CommentsController : Controller
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCommentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Details", "Courses", new { id = model.CourseId });
        }

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
        {
            return RedirectToAction("Login", "Account");
        }

        await _commentService.AddAsync(model, userId);

        return RedirectToAction("Details", "Courses", new { id = model.CourseId });
    }
}