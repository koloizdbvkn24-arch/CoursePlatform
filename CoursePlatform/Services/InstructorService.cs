using CoursePlatform.Data;
using CoursePlatform.Models.Entities;
using CoursePlatform.Services.Interfaces;
using CoursePlatform.ViewModels.Instructors;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoursePlatform.Services;

public class InstructorService : IInstructorService
{
    private readonly AppDbContext _context;

    public InstructorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<InstructorViewModel>> GetAllAsync()
    {
        return await _context.Instructors
            .AsNoTracking()
            .Select(x => new InstructorViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                Bio = x.Bio,
                Email = x.Email
            })
            .ToListAsync();
    }

    public async Task<List<SelectListItem>> GetSelectListAsync()
    {
        return await _context.Instructors
            .AsNoTracking()
            .OrderBy(x => x.FullName)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName
            })
            .ToListAsync();
    }

    public async Task<InstructorViewModel?> GetByIdAsync(int id)
    {
        return await _context.Instructors
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new InstructorViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                Bio = x.Bio,
                Email = x.Email
            })
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(InstructorViewModel model)
    {
        var instructor = new Instructor
        {
            FullName = model.FullName,
            Bio = model.Bio,
            Email = model.Email
        };

        _context.Instructors.Add(instructor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(InstructorViewModel model)
    {
        var instructor = await _context.Instructors.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (instructor is null)
        {
            return;
        }

        instructor.FullName = model.FullName;
        instructor.Bio = model.Bio;
        instructor.Email = model.Email;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var instructor = await _context.Instructors.FirstOrDefaultAsync(x => x.Id == id);
        if (instructor is null)
        {
            return;
        }

        _context.Instructors.Remove(instructor);
        await _context.SaveChangesAsync();
    }
}