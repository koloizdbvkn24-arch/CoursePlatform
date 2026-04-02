using System.Security.Cryptography;
using System.Text;
using CoursePlatform.Data;
using CoursePlatform.Models.Entities;
using CoursePlatform.Services.Interfaces;
using CoursePlatform.ViewModels.Account;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string? ErrorMessage)> RegisterAsync(RegisterViewModel model)
    {
        var usernameExists = await _context.Users
            .AnyAsync(x => x.Username.ToLower() == model.Username.ToLower());

        if (usernameExists)
        {
            return (false, "Username is already taken.");
        }

        var emailExists = await _context.Users
            .AnyAsync(x => x.Email.ToLower() == model.Email.ToLower());

        if (emailExists)
        {
            return (false, "Email is already registered.");
        }

        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            PasswordHash = HashPassword(model.Password),
            Role = "User"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return (true, null);
    }

    public async Task<int?> ValidateUserAsync(string emailOrUsername, string password)
    {
        var passwordHash = HashPassword(password);

        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                (x.Email == emailOrUsername || x.Username == emailOrUsername) &&
                x.PasswordHash == passwordHash);

        return user?.Id;
    }

    public async Task<UserProfileViewModel?> GetProfileAsync(int userId)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(x => x.Id == userId)
            .Select(x => new UserProfileViewModel
            {
                Username = x.Username,
                Email = x.Email,
                Role = x.Role
            })
            .FirstOrDefaultAsync();
    }

    private static string HashPassword(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }
}