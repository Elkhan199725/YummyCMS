using Microsoft.EntityFrameworkCore;
using YummyEnhanced.Models;

namespace YummyEnhanced.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Banner> Banners { get; set; }
    public DbSet<About> Abouts { get; set; }

    /// <summary>
    /// Asynchronously saves all changes made in this context to the underlying database, updating audit fields on
    /// tracked entities as appropriate.
    /// </summary>
    /// <remarks>For entities derived from BaseEntity, the CreatedDate property is set to the current UTC time
    /// when the entity is added, and the UpdatedDate property is set to the current UTC time when the entity is
    /// modified. This method overrides the base implementation to automatically manage these audit fields.</remarks>
    /// <param name="cancellationToken">A cancellation token that can be used to request cancellation of the asynchronous save operation.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries
    /// written to the database.</returns>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity.CreatedDate == default)
                    {
                        entry.Entity.CreatedDate = DateTimeOffset.UtcNow;
                    }
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedDate = DateTimeOffset.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
