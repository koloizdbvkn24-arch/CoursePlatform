using CoursePlatform.ViewModels.Comments;

namespace CoursePlatform.Services.Interfaces;

public interface ICommentService
{
    Task<List<CommentItemViewModel>> GetByCourseIdAsync(int courseId);
    Task AddAsync(CreateCommentViewModel model, int userId);
}