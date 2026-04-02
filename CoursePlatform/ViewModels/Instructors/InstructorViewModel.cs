using System.ComponentModel.DataAnnotations;

namespace CoursePlatform.ViewModels.Instructors;

public class InstructorViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string FullName { get; set; } = default!;

    [Required]
    [StringLength(2000)]
    public string Bio { get; set; } = default!;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = default!;
}