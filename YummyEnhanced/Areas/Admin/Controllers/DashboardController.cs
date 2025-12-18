using Microsoft.AspNetCore.Mvc;

namespace YummyEnhanced.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
