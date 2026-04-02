using CoursePlatform.ViewModels.Account;

namespace CoursePlatform.Services.Interfaces;

public interface IUserService
{
    Task<(bool Success, string? ErrorMessage)> RegisterAsync(RegisterViewModel model);
    Task<int?> ValidateUserAsync(string emailOrUsername, string password);
    Task<UserProfileViewModel?> GetProfileAsync(int userId);
}