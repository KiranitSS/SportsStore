using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using SportsStore.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

AddContextes(builder);

builder.Services.AddRazorPages();

AddServices(builder);

builder.Services.AddSession();

var app = builder.Build();

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/error");
}

app.UseRequestLocalization(opts =>
{
    opts.AddSupportedCultures("en-US")
    .AddSupportedUICultures("en-US")
    .SetDefaultCulture("en-US");
});

app.UseStaticFiles();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

MapRoutes(app);

app.MapDefaultControllerRoute();
app.MapRazorPages();

SeedData.EnsurePopulated(app);
await IdentitySeedData.EnsurePopulated(app);

app.Run();

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
    builder.Services.AddScoped<Cart>(SessionCart.GetCart);
    builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
}

static void MapRoutes(WebApplication app)
{
    app.MapControllerRoute(
        name: "paginator",
        pattern: "Products/Page{productPage:int}",
        defaults: new { Controller = "Home", Action = "Index", productPage = 1 });

    app.MapControllerRoute(
        name: "categoryPage",
        pattern: "{category}/Page{productPage:int}",
        defaults: new { Controller = "Home", Action = "Index" });

    app.MapControllerRoute(
        name: "category",
        pattern: "Products/{category}",
        defaults: new { Controller = "Home", action = "Index", productPage = 1 });

    app.MapControllerRoute(
        name: "shoppingCart",
        pattern: "Cart",
        defaults: new { Controller = "Cart", action = "Index" });

    app.MapControllerRoute(
        name: "default",
        pattern: "/",
        defaults: new { Controller = "Home", action = "Index" });

    app.MapControllerRoute(
        name: "checkout",
        pattern: "Checkout",
        defaults: new { Controller = "Order", action = "Checkout" });

    app.MapControllerRoute(
        name: "remove",
        pattern: "Remove",
        defaults: new { Controller = "Cart", action = "Remove" });
}

static void AddContextes(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<StoreDbContext>(opts =>
    {
        opts.UseSqlServer(builder.Configuration["ConnectionStrings:SportsStoreConnection"]);
    });

    builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityConnection"]);
    });
}
