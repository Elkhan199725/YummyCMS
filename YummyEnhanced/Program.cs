using Microsoft.EntityFrameworkCore;
using YummyEnhanced.Data;
using YummyEnhanced.Services.Implementations; // Add this
using YummyEnhanced.Services.Interfaces;      // Add this

var builder = WebApplication.CreateBuilder(args);

// 1. Add Framework Services
builder.Services.AddControllersWithViews();

// 2. Add Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. REGISTER YOUR CUSTOM SERVICES HERE
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IBannerService, BannerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Areas Route (Admin)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// Default Route (User)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();