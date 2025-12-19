using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using YummyEnhanced.Models;
using YummyEnhanced.Services.Interfaces;
using YummyEnhanced.ViewModels;

namespace YummyEnhanced.Controllers;

public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}
