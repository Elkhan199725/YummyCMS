using Microsoft.AspNetCore.Mvc;
using YummyEnhanced.Services.Interfaces;

namespace YummyEnhanced.ViewComponents;

public class BannersViewComponent : ViewComponent
{
    private readonly IBannerService _bannerService;
    public BannersViewComponent(IBannerService bannerService)
    {
        _bannerService = bannerService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var data = await _bannerService.GetAllUIAsync();
        return View(data);
    }
}
