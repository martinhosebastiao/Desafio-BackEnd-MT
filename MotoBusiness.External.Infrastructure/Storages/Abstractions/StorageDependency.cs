using Microsoft.Extensions.DependencyInjection;
using MotoBusiness.External.Infrastructure.Storages.Local;
using MotoBusiness.Internal.Domain.Core.Abstractions;

namespace MotoBusiness.External.Infrastructure.Storages.Abstractions
{
    public static class StorageDependency
	{
        public static IServiceCollection AddStorage(
            this IServiceCollection services)
        {  
            services.AddScoped<IStorageService, StorageService>();

            return services;
        }
    }
}

