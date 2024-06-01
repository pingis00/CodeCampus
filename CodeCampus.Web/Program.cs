
using CodeCampus.Infrastructure.Contexts;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Helpers.MiddleWares;
using CodeCampus.Web.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllersWithViews();
services.AddRouting(x => x.LowercaseUrls = true);

services.AddDbContext<DataContext>(x => x.UseSqlServer(configuration.GetConnectionString("SqlServer")));

services.AddHttpContextAccessor();
services.AddHttpClient();
builder.Services.RegisterServices(builder.Configuration);


services.AddDefaultIdentity<UserEntity>(x =>
{
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequireDigit = true;
    x.Password.RequiredLength = 8;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = true;
    x.Password.RequireLowercase = true;
    x.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole>()
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

services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = "161635572771-gq43sqj3l5iaqfrfatfvte4dcv3tm9hp.apps.googleusercontent.com";
        googleOptions.ClientSecret = "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";
    })
    .AddFacebook(x =>
    {
        x.AppId = "1496868827911909";
        x.AppSecret = "ec545aa4b8fa5ece5ffe500589aa3a4b";
        x.Fields.Add("first_name");
        x.Fields.Add("last_name");
    });

var googleMapsApiKey = configuration["GoogleMapsApiKey"];

var app = builder.Build();
app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseUserSessionValidation();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = ["Admin"];
    foreach (var role in roles)
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
