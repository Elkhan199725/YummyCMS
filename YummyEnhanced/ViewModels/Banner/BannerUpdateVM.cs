using System.ComponentModel.DataAnnotations;

namespace YummyEnhanced.ViewModels.Banner;

public sealed class BannerUpdateVM
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Description is required")]
    [MaxLength(500)]
    public string Description { get; set; } = null!;

    public IFormFile? Image { get; set; }

    public string? ExistingImageUrl { get; set; }

    [Url]
    public string? VideoUrl { get; set; }

    [MaxLength(50)]
    public string ButtonText { get; set; } = null!;

    [MaxLength(200)]
    public string ButtonLink { get; set; } = null!;
}
