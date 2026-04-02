using CoursePlatform.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        if (!await context.Categories.AnyAsync())
        {
            var categories = new List<Category>
            {
                new()
                {
                    Name = "Programming",
                    Description = "Courses about software development, coding, and programming languages."
                },
                new()
                {
                    Name = "Design",
                    Description = "Courses about UI/UX design, graphic design, and creative tools."
                },
                new()
                {
                    Name = "Marketing",
                    Description = "Courses about digital marketing, branding, and promotion."
                }
            };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        if (!await context.Instructors.AnyAsync())
        {
            var instructors = new List<Instructor>
            {
                new()
                {
                    FullName = "John Smith",
                    Bio = "Senior .NET developer with practical experience in ASP.NET Core and enterprise systems.",
                    Email = "john.smith@example.com"
                },
                new()
                {
                    FullName = "Emma Johnson",
                    Bio = "UI/UX designer and mentor with experience in modern design systems.",
                    Email = "emma.johnson@example.com"
                },
                new()
                {
                    FullName = "Michael Brown",
                    Bio = "Digital marketing specialist focused on SEO, content strategy, and analytics.",
                    Email = "michael.brown@example.com"
                }
            };

            context.Instructors.AddRange(instructors);
            await context.SaveChangesAsync();
        }

        if (!await context.Users.AnyAsync())
        {
            var adminPasswordHash = HashPassword("Admin123!");
            var userPasswordHash = HashPassword("User123!");

            var users = new List<User>
            {
                new()
                {
                    Username = "admin",
                    Email = "admin@courseplatform.com",
                    PasswordHash = adminPasswordHash,
                    Role = "Admin"
                },
                new()
                {
                    Username = "student1",
                    Email = "student1@courseplatform.com",
                    PasswordHash = userPasswordHash,
                    Role = "User"
                },
                new()
                {
                    Username = "student2",
                    Email = "student2@courseplatform.com",
                    PasswordHash = userPasswordHash,
                    Role = "User"
                }
            };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }

        if (!await context.Courses.AnyAsync())
        {
            var programmingCategory = await context.Categories.FirstAsync(x => x.Name == "Programming");
            var designCategory = await context.Categories.FirstAsync(x => x.Name == "Design");
            var marketingCategory = await context.Categories.FirstAsync(x => x.Name == "Marketing");

            var john = await context.Instructors.FirstAsync(x => x.Email == "john.smith@example.com");
            var emma = await context.Instructors.FirstAsync(x => x.Email == "emma.johnson@example.com");
            var michael = await context.Instructors.FirstAsync(x => x.Email == "michael.brown@example.com");

            var courses = new List<Course>
            {
                new()
                {
                    Title = "ASP.NET Core MVC for Beginners",
                    Description = "Learn how to build web applications using ASP.NET Core MVC, Entity Framework Core, and layered architecture.",
                    Price = 49.99m,
                    DurationHours = 24,
                    Level = "Beginner",
                    CreatedAt = DateTime.UtcNow,
                    CategoryId = programmingCategory.Id,
                    InstructorId = john.Id
                },
                new()
                {
                    Title = "C# Fundamentals",
                    Description = "A practical introduction to C# syntax, OOP principles, collections, LINQ, and clean code basics.",
                    Price = 39.99m,
                    DurationHours = 18,
                    Level = "Beginner",
                    CreatedAt = DateTime.UtcNow,
                    CategoryId = programmingCategory.Id,
                    InstructorId = john.Id
                },
                new()
                {
                    Title = "UI/UX Design Essentials",
                    Description = "Understand the core principles of user interface and user experience design, wireframing, and prototyping.",
                    Price = 44.99m,
                    DurationHours = 20,
                    Level = "Intermediate",
                    CreatedAt = DateTime.UtcNow,
                    CategoryId = designCategory.Id,
                    InstructorId = emma.Id
                },
                new()
                {
                    Title = "Digital Marketing Basics",
                    Description = "Learn SEO, social media strategies, content marketing, and campaign performance analysis.",
                    Price = 34.99m,
                    DurationHours = 16,
                    Level = "Beginner",
                    CreatedAt = DateTime.UtcNow,
                    CategoryId = marketingCategory.Id,
                    InstructorId = michael.Id
                }
            };

            context.Courses.AddRange(courses);
            await context.SaveChangesAsync();
        }

        if (!await context.Comments.AnyAsync())
        {
            var firstCourse = await context.Courses.OrderBy(x => x.Id).FirstAsync();
            var secondCourse = await context.Courses.OrderBy(x => x.Id).Skip(1).FirstAsync();
            var firstUser = await context.Users.FirstAsync(x => x.Username == "student1");
            var secondUser = await context.Users.FirstAsync(x => x.Username == "student2");

            var comments = new List<Comment>
            {
                new()
                {
                    Content = "Very useful course for understanding the basics of MVC.",
                    CreatedAt = DateTime.UtcNow,
                    CourseId = firstCourse.Id,
                    UserId = firstUser.Id
                },
                new()
                {
                    Content = "The explanations are clear and easy to follow.",
                    CreatedAt = DateTime.UtcNow,
                    CourseId = firstCourse.Id,
                    UserId = secondUser.Id
                },
                new()
                {
                    Content = "Great introduction to C# for beginners.",
                    CreatedAt = DateTime.UtcNow,
                    CourseId = secondCourse.Id,
                    UserId = firstUser.Id
                }
            };

            context.Comments.AddRange(comments);
            await context.SaveChangesAsync();
        }
    }

    private static string HashPassword(string password)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}