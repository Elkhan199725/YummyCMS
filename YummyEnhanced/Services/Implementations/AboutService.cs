using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;
using YummyEnhanced.Data;
using YummyEnhanced.Models;
using YummyEnhanced.Services.Interfaces;

namespace YummyEnhanced.Services.Implementations;

public class AboutService : IAboutService
{
    private readonly AppDbContext _context;
    private readonly IFileService _fileService;

    public AboutService(AppDbContext context, IFileService fileService  )
    {
        _context = context;
        _fileService = fileService;
    }
    public async Task<About> GetAboutAsync()
    {
        var data = await _context.Abouts.FirstOrDefaultAsync();

        if (data is null)
        {
            data = new About
            {
                Title = "About Us",
                Header = "Welcome to Yummy",
                Description = "This is default text. Please go to Admin Panel to update it.",
                ImageUrl = "about.jpg"
            };

            _context.Abouts.Add(data);
            await _context.SaveChangesAsync();
        }

        return data;
    }

    public async Task UpdateAboutAsync(About about, IFormFile? file)
    {
        var existingAbout = await _context.Abouts.FindAsync(about.Id);

        if (existingAbout == null) return;


        existingAbout.Title = about.Title;
        existingAbout.Header = about.Header;
        existingAbout.Description = about.Description;

        existingAbout.PhoneNumber = about.PhoneNumber;
        existingAbout.CEOName = about.CEOName;
        existingAbout.CEOJob = about.CEOJob;

        if(file is not null)
        {
            if (!string.IsNullOrEmpty(existingAbout.ImageUrl))
            {
                _fileService.Delete(existingAbout.ImageUrl, "about");
            }

            existingAbout.ImageUrl = await _fileService.UploadAsync(file, "about");
        }

        await _context.SaveChangesAsync();
    }
}
