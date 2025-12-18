using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using YummyEnhanced.Models;
using YummyEnhanced.Services.Interfaces;
using YummyEnhanced.ViewModels;

namespace YummyEnhanced.Controllers;

public class HomeController : Controller
{
    private readonly IBannerService _bannerService;
    public HomeController(IBannerService bannerService)
    {
        _bannerService = bannerService;
    }
    public async Task<IActionResult> Index()
    {
        var banners = await _bannerService.GetAllUIAsync();

        var model = new HomeVM
        {
            Banners = banners
        };

        return View(model);
    }
}
