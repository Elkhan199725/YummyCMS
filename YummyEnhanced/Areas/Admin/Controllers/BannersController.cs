using Microsoft.AspNetCore.Mvc;
using YummyEnhanced.Extensions; // Ensure this matches your namespace
using YummyEnhanced.Services.Interfaces;
using YummyEnhanced.ViewModels.Banner;

namespace YummyEnhanced.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannersController : Controller
    {
        private readonly IBannerService _bannerService;

        public BannersController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var banners = await _bannerService.GetAllAdminAsync();
            return View(banners);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var existingBanners = await _bannerService.GetAllAdminAsync();
            if (existingBanners.Count > 0)
            {
                TempData["Error"] = "You can only have one active banner. Please delete or edit the existing one.";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BannerCreateVM model)
        {
            var existingBanners = await _bannerService.GetAllAdminAsync();
            if (existingBanners.Count > 0)
            {
                TempData["Error"] = "Banner limit reached.";
                return RedirectToAction(nameof(Index));
            }

            if (model.Image != null)
            {
                if (!model.Image.IsImage())
                {
                    ModelState.AddModelError("Image", "Only image files (jpg, png, webp) are allowed.");
                }
                else if (!model.Image.IsAllowedSize(5))
                {
                    ModelState.AddModelError("Image", "Image size must be less than 5MB.");
                }
            }

            if (!ModelState.IsValid) return View(model);

            try
            {
                await _bannerService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to create banner: " + ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            try
            {
                var model = await _bannerService.GetByIdAsync(id);
                return View(model);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(BannerUpdateVM model)
        {
            if (model.Image != null)
            {
                if (!model.Image.IsImage())
                {
                    ModelState.AddModelError("Image", "Only image files are allowed.");
                }
                else if (!model.Image.IsAllowedSize(5))
                {
                    ModelState.AddModelError("Image", "Image size must be less than 5MB.");
                }
            }

            if (!ModelState.IsValid) return View(model);

            try
            {
                await _bannerService.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to update banner: " + ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var model = await _bannerService.GetByIdAsync(id);
                return View(model);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bannerService.SoftDeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Trash()
        {
            var deletedBanners = await _bannerService.GetDeletedBannersAsync();
            return View(deletedBanners);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            var activeBanners = await _bannerService.GetAllAdminAsync();
            if (activeBanners.Count > 0)
            {
                TempData["Error"] = "Cannot restore this banner because an active banner already exists. Please delete the active one first.";
                return RedirectToAction(nameof(Trash));
            }

            await _bannerService.RestoreAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> HardDelete(Guid id)
        {
            var deletedBanners = await _bannerService.GetDeletedBannersAsync();
            var banner = deletedBanners.FirstOrDefault(b => b.Id == id);

            if (banner == null) return NotFound();

            return View(banner);
        }

        [HttpPost, ActionName("HardDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HardDeleteConfirmed(Guid id)
        {
            await _bannerService.HardDeleteAsync(id);
            return RedirectToAction(nameof(Trash));
        }
    }
}