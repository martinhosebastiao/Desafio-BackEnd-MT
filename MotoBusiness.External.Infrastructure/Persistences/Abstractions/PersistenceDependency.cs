using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotoBusiness.External.Infrastructure.Persistences.Contexts;
using MotoBusiness.External.Infrastructure.Persistences.Repositores;
using MotoBusiness.External.Infrastructure.Persistences.Repositores.Abstractions;
using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Deliverers;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;
using MotoBusiness.Internal.Domain.Core.Entities.Rentals;

namespace MotoBusiness.External.Infrastructure.Persistences.Abstractions
{
	public static class PersistenceDependency
	{
		public static IServiceCollection AddPersistence(
			this IServiceCollection services, IConfiguration configuration)
		{
			var npgConnection = configuration
				.GetConnectionString("PostgreConnection");

			services.AddDbContext<MainContext>(
				options => options.UseNpgsql(npgConnection));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddScoped<IMotorbikeRepository, MotorbikeRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();

            return services;
		}
	}
}

