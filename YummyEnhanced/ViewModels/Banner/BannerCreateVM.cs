using System.ComponentModel.DataAnnotations;

namespace YummyEnhanced.ViewModels.Banner;

public sealed class BannerCreateVM
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Description is required")]
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Please select an image")]
    public IFormFile Image { get; set; } = null!;

    [Url(ErrorMessage = "Please enter a valid URL")]
    public string? VideoUrl { get; set; } = null!;

    [MaxLength(50)]
    public string ButtonText { get; set; } = "Book a Table";

    [MaxLength(200)]
    public string ButtonLink { get; set; } = "#book-a-table";
}
