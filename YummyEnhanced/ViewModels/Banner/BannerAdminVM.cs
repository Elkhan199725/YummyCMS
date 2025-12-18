namespace YummyEnhanced.ViewModels.Banner;

public sealed class BannerAdminVM
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string CreatedDate { get; set; } = null!;
}
