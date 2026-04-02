namespace CoursePlatform.Models.Entities;

public class Comment
{
    public int Id { get; set; }

    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int CourseId { get; set; }
    public Course Course { get; set; } = default!;

    public int UserId { get; set; }
    public User User { get; set; } = default!;
}