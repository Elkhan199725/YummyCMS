using YummyEnhanced.Models;

namespace YummyEnhanced.Services.Interfaces;

public interface IAboutService
{
    Task<About> GetAboutAsync();
    Task UpdateAboutAsync(About about, IFormFile? file);
}
