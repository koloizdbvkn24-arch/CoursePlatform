using System.ComponentModel.DataAnnotations;

namespace CoursePlatform.ViewModels.Categories;

public class CategoryViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = default!;

    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = default!;
}