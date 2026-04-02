namespace CoursePlatform.ViewModels.Comments;

public class CommentItemViewModel
{
    public int Id { get; set; }
    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public string Username { get; set; } = default!;
}