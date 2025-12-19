using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using YummyEnhanced.Extensions;
using YummyEnhanced.Models;
using YummyEnhanced.Services.Interfaces;
using YummyEnhanced.ViewModels.About;

namespace YummyEnhanced.Areas.Admin.Controllers;

[Area("Admin")]
public sealed class AboutController : Controller
{
    private readonly IAboutService _aboutService;
    public AboutController(IAboutService aboutService)
    {
        _aboutService = aboutService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var data = await _aboutService.GetAboutAsync();

        if (data == null) return NotFound();

        var model = new AboutUpdateVM
        {
            Id = data.Id,
            Title = data.Title,
            Header = data.Header,
            Description = data.Description,

            ImageUrl = data.ImageUrl,

            PhoneNumber = data.PhoneNumber,
            CEOName = data.CEOName,
            CEOJob = data.CEOJob
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(AboutUpdateVM vm)
    {
        if (vm.Image is not null)
        {
            if (!vm.Image.IsImage())
            {
                ModelState.AddModelError("Image", "Only image files (jpg, png, webp) are allowed.");
            }
            else if (!vm.Image.IsAllowedSize(5))
            {
                ModelState.AddModelError("Image", "Image size must be less than 5MB.");
            }
        }

        if (!ModelState.IsValid) return View(vm);

        About about = new About
        {
            Id = vm.Id,
            Title = vm.Title,
            Header = vm.Header,
            Description = vm.Description,
            PhoneNumber = vm.PhoneNumber,
            CEOName = vm.CEOName,
            CEOJob = vm.CEOJob
        };

        try
        {
            await _aboutService.UpdateAboutAsync(about, vm.Image);

            TempData["Success"] = "About section updated successfully!";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Failed to update: " + ex.Message);
            return View(vm);
        }
    }
}