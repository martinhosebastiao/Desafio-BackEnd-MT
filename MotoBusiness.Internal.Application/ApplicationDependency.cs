using Microsoft.Extensions.DependencyInjection;
using MotoBusiness.Internal.Application.Deliverers;
using MotoBusiness.Internal.Application.Motorbikes;
using MotoBusiness.Internal.Application.Rentals;

namespace MotoBusiness.Internal.Application
{
    public static class ApplicationDependency
	{
        public static IServiceCollection AddApplication(
			this IServiceCollection services)
        {
            services.AddScoped<IDeliveryApp, DeliveryApp>();
            services.AddScoped<IMotorbikeApp, MotorbikeApp>();
            services.AddScoped<IRentalApp, RentalApp>();

            return services;
		}
	}
}

