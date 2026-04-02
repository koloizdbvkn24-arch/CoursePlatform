using System.ComponentModel.DataAnnotations;

namespace CoursePlatform.ViewModels.Account;

public class LoginViewModel
{
    [Required]
    public string EmailOrUsername { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;
}