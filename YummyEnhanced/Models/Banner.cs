using System.ComponentModel.DataAnnotations;

namespace YummyEnhanced.Models;

public sealed class Banner : BaseEntity
{
    [Required, MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required, MaxLength(500)]
    public string Description { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    public string? VideoUrl { get; set; } = null!;

    [MaxLength(50)]
    public string ButtonText { get; set; } = "Book a Table";

    [MaxLength(200)]
    public string ButtonLink { get; set; } = "#book-a-table";

    public override string ToString()
    {
        return $"Banner: {Title} (ID: {Id})";
    }
}