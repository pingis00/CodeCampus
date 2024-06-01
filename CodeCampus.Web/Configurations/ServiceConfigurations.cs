using CodeCampus.Infrastructure.Interfaces.Repositories;
using CodeCampus.Infrastructure.Interfaces.Services;
using CodeCampus.Infrastructure.Repositories;
using CodeCampus.Infrastructure.Services;

namespace CodeCampus.Web.Configurations;

public static class ServiceConfiguration
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ISubscribeRepository, SubscribeRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();



        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<ISubscribeService, SubscribeService>();
        services.AddScoped<IContactService, ContactService>();

    }
}
