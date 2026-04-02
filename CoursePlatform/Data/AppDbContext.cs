using CoursePlatform.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Instructor>()
            .Property(x => x.FullName)
            .HasMaxLength(150)
            .IsRequired();

        modelBuilder.Entity<Instructor>()
            .Property(x => x.Email)
            .HasMaxLength(150)
            .IsRequired();

        modelBuilder.Entity<Course>()
            .Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        modelBuilder.Entity<Course>()
            .Property(x => x.Level)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Comment>()
            .Property(x => x.Content)
            .HasMaxLength(1000)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(x => x.Username)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(x => x.Email)
            .HasMaxLength(150)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(x => x.PasswordHash)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(x => x.Role)
            .HasMaxLength(20)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Username)
            .IsUnique();

        modelBuilder.Entity<Course>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Courses)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
            .HasOne(x => x.Instructor)
            .WithMany(x => x.Courses)
            .HasForeignKey(x => x.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(x => x.Course)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(x => x.User)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}