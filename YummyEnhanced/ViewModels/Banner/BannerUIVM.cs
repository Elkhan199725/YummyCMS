namespace YummyEnhanced.ViewModels.Banner;

public sealed class BannerUIVM
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string? VideoUrl { get; set; } = null!;
    public string ButtonText { get; set; } = null!;
    public string ButtonLink { get; set; } = null!;
}
