using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoursePlatform.ViewModels.Courses;

public class EditCourseViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = default!;

    [Required]
    [StringLength(3000)]
    public string Description { get; set; } = default!;

    [Range(0, 100000)]
    public decimal Price { get; set; }

    [Range(1, 1000)]
    public int DurationHours { get; set; }

    [Required]
    [StringLength(50)]
    public string Level { get; set; } = default!;

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }

    [Range(1, int.MaxValue)]
    public int InstructorId { get; set; }

    public List<SelectListItem> Categories { get; set; } = new();
    public List<SelectListItem> Instructors { get; set; } = new();
}