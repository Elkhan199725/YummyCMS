using YummyEnhanced.ViewModels.Banner;

namespace YummyEnhanced.Services.Interfaces;

public interface IBannerService
{
    Task<List<BannerAdminVM>> GetAllAdminAsync();
    Task<BannerUpdateVM> GetByIdAsync(Guid id);
    Task CreateAsync(BannerCreateVM model);
    Task UpdateAsync(BannerUpdateVM model);
    Task SoftDeleteAsync(Guid id);
    Task HardDeleteAsync(Guid id);
    Task<List<BannerUIVM>> GetAllUIAsync();
    Task<List<BannerAdminVM>> GetDeletedBannersAsync();
    Task RestoreAsync(Guid id);
}
