using CoursePlatform.Data;
using CoursePlatform.Models.Entities;
using CoursePlatform.Services.Interfaces;
using CoursePlatform.ViewModels.Comments;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Services;

public class CommentService : ICommentService
{
    private readonly AppDbContext _context;

    public CommentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CommentItemViewModel>> GetByCourseIdAsync(int courseId)
    {
        return await _context.Comments
            .AsNoTracking()
            .Include(x => x.User)
            .Where(x => x.CourseId == courseId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new CommentItemViewModel
            {
                Id = x.Id,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                Username = x.User.Username
            })
            .ToListAsync();
    }

    public async Task AddAsync(CreateCommentViewModel model, int userId)
    {
        var courseExists = await _context.Courses.AnyAsync(x => x.Id == model.CourseId);
        if (!courseExists)
        {
            return;
        }

        var comment = new Comment
        {
            Content = model.Content,
            CreatedAt = DateTime.UtcNow,
            CourseId = model.CourseId,
            UserId = userId
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }
}