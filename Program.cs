using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using SportStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:SportsStoreConnection"]);
});

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
builder.Services.AddScoped<IOrderRepository,EFOrderRepository>();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddServerSideBlazor(); // Blazor

var app = builder.Build();

app.UseStaticFiles();

app.UseSession();
app.MapControllerRoute(
    "catpage",
    "{category}/Page{productPage:int}",
    new { Controller = "Home", action = "Index" });

app.MapControllerRoute(
    "page",
    "Page{productPage:int}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute(
    "category",
    "{category}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute(
    "pagination",
    "Products/Page{productPage}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.MapBlazorHub(); // Blazor  {MapBlazorHub method registers the Blazor middleware components}
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index"); // Blazor

SeedData.EnsurePopulated(app);
app.Run();
