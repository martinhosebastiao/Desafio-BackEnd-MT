using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotoBusiness.External.Infrastructure.Messaging.Absctrations;
using MotoBusiness.External.Infrastructure.Persistences.Abstractions;
using MotoBusiness.External.Infrastructure.Storages.Abstractions;
using MotoBusiness.Internal.Application;

namespace MotoBusiness.External.Infrastructure.IoC;

public static class IoC
{
    public static IServiceCollection AddDependences(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStorage();
        services.AddMessaging();
        services.AddApplication();
        services.AddPersistence(configuration);

        return services;
    }
}