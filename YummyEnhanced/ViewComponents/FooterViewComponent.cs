using Microsoft.AspNetCore.Mvc;

namespace YummyEnhanced.ViewComponents;

public class FooterViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
