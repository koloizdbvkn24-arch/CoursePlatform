namespace CoursePlatform.ViewModels.Courses;

public class CourseListItemViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;
    public string ShortDescription { get; set; } = default!;
    public decimal Price { get; set; }
    public int DurationHours { get; set; }
    public string Level { get; set; } = default!;

    public string CategoryName { get; set; } = default!;
    public string InstructorName { get; set; } = default!;
}