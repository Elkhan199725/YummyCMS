using Microsoft.AspNetCore.Mvc;

namespace YummyEnhanced.ViewComponents;

public class HeaderViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(); 
    }
}
