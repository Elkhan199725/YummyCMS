using YummyEnhanced.ViewModels.Banner;

namespace YummyEnhanced.ViewModels;

public sealed class HomeVM
{
    public List<BannerUIVM> Banners { get; set; } = new List<BannerUIVM>();
}
