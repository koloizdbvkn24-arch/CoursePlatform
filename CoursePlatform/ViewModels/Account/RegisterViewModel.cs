using System.ComponentModel.DataAnnotations;

namespace CoursePlatform.ViewModels.Account;

public class RegisterViewModel
{
    [Required]
    [StringLength(100)]
    public string Username { get; set; } = default!;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = default!;
}