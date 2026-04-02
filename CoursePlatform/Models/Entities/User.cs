namespace CoursePlatform.Models.Entities;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Role { get; set; } = "User";

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}