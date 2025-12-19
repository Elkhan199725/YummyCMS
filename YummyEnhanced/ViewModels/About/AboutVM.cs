namespace YummyEnhanced.ViewModels.About;

public sealed class AboutVM
{
    public string Title { get; set; } = null!;
    public string Header { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CEOName { get; set; }
    public string? CEOJob { get; set; }
}
