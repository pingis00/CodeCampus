using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Interfaces.Repositories;
using CodeCampus.Infrastructure.Interfaces.Services;
using CodeCampus.Infrastructure.Interfaces.Services.Admin;
using CodeCampus.Infrastructure.Repositories;
using CodeCampus.Infrastructure.Services;
using CodeCampus.Infrastructure.Services.Admin;
using Microsoft.AspNetCore.Identity;

namespace CodeCampus_WebApi.Configurations;

public static class ServiceConfiguration
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<UserManager<UserEntity>>();
        services.AddScoped<SignInManager<UserEntity>>();

        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<ISubscribeRepository, SubscribeRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUserCourseRepository, UserCourseRepository>();

        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<ISubscribeService, SubscribeService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IUserCourseService, UserCourseService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddTransient<IFileUploadService, FileUploadService>();
        services.AddTransient<ICategoryService, CategoryService>();


        services.AddHttpClient<IApiCourseService, ApiCourseService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7297/");
        });
    }
}
