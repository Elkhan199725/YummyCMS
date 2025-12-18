using Microsoft.EntityFrameworkCore;
using YummyEnhanced.Data;
using YummyEnhanced.Models;
using YummyEnhanced.Services.Interfaces;
using YummyEnhanced.ViewModels.Banner;

namespace YummyEnhanced.Services.Implementations;

public class BannerService : IBannerService
{
    private readonly AppDbContext _context;
    private readonly IFileService _fileService;

    public BannerService(AppDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }
    public async Task CreateAsync(BannerCreateVM model)
    {
        string imageName = await _fileService.UploadAsync(model.Image, "banners");

        Banner banner = new Banner
        {
            Title = model.Title,
            Description = model.Description,
            ImageUrl = imageName,
            VideoUrl = model.VideoUrl!,
            ButtonText = model.ButtonText,
            ButtonLink = model.ButtonLink
        };
        
        await _context.Banners.AddAsync(banner);
        await _context.SaveChangesAsync();
    }

    public async Task<List<BannerAdminVM>> GetAllAdminAsync()
    {
        List<BannerAdminVM> adminBanners = await _context.Banners
            .Where(b => !b.IsDeleted)
            .OrderByDescending(b => b.CreatedDate)
            .Select(b => new BannerAdminVM
            {
                Id = b.Id,
                Title = b.Title,
                ImageUrl = b.ImageUrl,
                CreatedDate = b.CreatedDate.ToString("dd MMM yyyy")
            }).ToListAsync();

        return adminBanners;
    }

    public async Task<List<BannerUIVM>> GetAllUIAsync()
    {
        List<BannerUIVM> uiBanners = await _context.Banners
                            .Where(b => !b.IsDeleted)
                            .OrderByDescending(b => b.CreatedDate)
                            .Select(b => new BannerUIVM
                            {
                                Title = b.Title,
                                Description = b.Description,
                                ImageUrl = b.ImageUrl,
                                VideoUrl = b.VideoUrl,
                                ButtonLink = b.ButtonLink,
                                ButtonText = b.ButtonText
                            }).ToListAsync();

        return uiBanners;
    }

    public async Task<BannerUpdateVM> GetByIdAsync(Guid id)
    {
        Banner? banner = await _context.Banners.FindAsync(id);

        if (banner is null)
            throw new KeyNotFoundException("Banner not found.");

        if (banner.IsDeleted)
            throw new InvalidOperationException("Banner is deleted.");


        BannerUpdateVM bannerModel = new BannerUpdateVM()
        {
            Id = banner.Id,
            Title = banner.Title,
            Description = banner.Description,
            ExistingImageUrl = banner.ImageUrl,
            VideoUrl = banner.VideoUrl,
            ButtonText = banner.ButtonText,
            ButtonLink = banner.ButtonLink
        };

        return bannerModel;
    }

    public async Task<List<BannerAdminVM>> GetDeletedBannersAsync()
    {
        var deletedBanners = await _context.Banners
            .Where(b => b.IsDeleted)
            .OrderByDescending(b => b.CreatedDate)
            .Select(b => new BannerAdminVM
            {
                Id = b.Id,
                Title = b.Title,
                ImageUrl = b.ImageUrl,
                CreatedDate = b.CreatedDate.ToString("dd MMM yyyy")
            })
            .ToListAsync();

        return deletedBanners;
    }

    public async Task HardDeleteAsync(Guid id)
    {
        var banner = await _context.Banners.FindAsync(id);
        if (banner is null) return;

        _fileService.Delete(banner.ImageUrl, "banners");

        _context.Banners.Remove(banner);
        
        await _context.SaveChangesAsync();
    }

    public async Task RestoreAsync(Guid id)
    {
        var deletedBanner = await _context.Banners.FindAsync(id);
        if (deletedBanner is null) return;

        deletedBanner.IsDeleted = false;
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        var banner = await _context.Banners.FindAsync(id);
        if (banner is null) return;

        banner.IsDeleted = true;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(BannerUpdateVM model)
    {
        Banner? banner = await _context.Banners.FindAsync(model.Id);

        if (banner is null)
            throw new KeyNotFoundException("Banner not found.");

        if (banner.IsDeleted)
            throw new InvalidOperationException("Banner is deleted.");

        banner.Title = model.Title;
        banner.Description = model.Description;
        banner.VideoUrl = model.VideoUrl!;
        banner.ButtonText = model.ButtonText;
        banner.ButtonLink = model.ButtonLink;

        if(model.Image is not null)
        {
            _fileService.Delete(banner.ImageUrl, "banners");

            string newImageName = await _fileService.UploadAsync(model.Image, "banners");
            banner.ImageUrl = newImageName;
        }

        _context.Banners.Update(banner);
        await _context.SaveChangesAsync();
    }
}
