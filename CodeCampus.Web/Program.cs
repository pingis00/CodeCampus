
using CodeCampus.Infrastructure.Contexts;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Helpers.MiddleWares;
using CodeCampus.Infrastructure.Repositories;
using CodeCampus.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllersWithViews();
services.AddRouting(x => x.LowercaseUrls = true);

services.AddDbContext<DataContext>(x => x.UseSqlServer(configuration.GetConnectionString("SqlServer")));
services.AddScoped<AddressRepository>();
services.AddScoped<AddressService>();

services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<DataContext>();
services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/signin";
    x.LogoutPath = "/signout";
    x.AccessDeniedPath = "/Home/AccessDenied";
    x.Cookie.HttpOnly = true;
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    x.SlidingExpiration = true;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

var googleMapsApiKey = configuration["GoogleMapsApiKey"];

var app = builder.Build();
app.UseHsts();
app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseUserSessionValidation();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
