using CoursePlatform.ViewModels.Comments;

namespace CoursePlatform.ViewModels.Courses;

public class CourseDetailsViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int DurationHours { get; set; }
    public string Level { get; set; } = default!;
    public DateTime CreatedAt { get; set; }

    public string CategoryName { get; set; } = default!;
    public string InstructorName { get; set; } = default!;
    public string InstructorEmail { get; set; } = default!;
    public string InstructorBio { get; set; } = default!;

    public List<CommentItemViewModel> Comments { get; set; } = new();
    public CreateCommentViewModel NewComment { get; set; } = new();
}