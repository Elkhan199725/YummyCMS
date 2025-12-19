using System.ComponentModel.DataAnnotations;

namespace YummyEnhanced.Models;

public class About
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Header { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    public string? ImageUrl { get; set; }
    public string? PhoneNumber { get; set; }

    public string? CEOName { get; set; }
    public string? CEOJob { get; set; }
}