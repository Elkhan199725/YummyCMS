using Microsoft.AspNetCore.Mvc;
using YummyEnhanced.Services.Interfaces;

namespace YummyEnhanced.ViewComponents;

public sealed class AboutViewComponent : ViewComponent
{
    private readonly IAboutService _aboutService;
    public AboutViewComponent(IAboutService aboutService)
    {
        _aboutService = aboutService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var data = await _aboutService.GetAboutAsync();

        return View(data);
    }
}
