using CodeCampus.Infrastructure.Helpers;
using CodeCampus.Infrastructure.Interfaces.Repositories;
using CodeCampus.Infrastructure.Interfaces.Services;
using CodeCampus.Infrastructure.Interfaces.Services.Admin;
using CodeCampus.Infrastructure.Repositories;
using CodeCampus.Infrastructure.Services;
using CodeCampus.Infrastructure.Services.Admin;

namespace CodeCampus.Web.Configurations;

public static class ServiceConfiguration
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ISubscribeRepository, SubscribeRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUserCourseRepository, UserCourseRepository>();



        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<ISubscribeService, SubscribeService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IAccountManager, AccountManager>();
        services.AddScoped<IAdminCourseService, AdminCourseService>();
        services.AddScoped<IAdminContactService, AdminContactService>();
        services.AddScoped<IAdminSubscribeService, AdminSubscribeService>();
        services.AddScoped<IUserCourseService, UserCourseService>();
        services.AddScoped<HttpClientHelper>();

        services.AddTransient<IFileUploadService, FileUploadService>();
        services.AddTransient<ICategoryService, CategoryService>();

        services.AddHttpClient<IApiCourseService, ApiCourseService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7297/");
        });

    }
}
