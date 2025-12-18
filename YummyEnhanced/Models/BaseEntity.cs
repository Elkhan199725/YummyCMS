using System.ComponentModel.DataAnnotations;

namespace YummyEnhanced.Models;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedDate { get; set; }
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}