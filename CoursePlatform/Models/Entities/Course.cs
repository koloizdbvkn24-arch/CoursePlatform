namespace CoursePlatform.Models.Entities;

public class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int DurationHours { get; set; }
    public string Level { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public int InstructorId { get; set; }
    public Instructor Instructor { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}