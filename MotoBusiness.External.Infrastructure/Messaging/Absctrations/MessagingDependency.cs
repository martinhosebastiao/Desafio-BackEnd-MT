using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotoBusiness.External.Infrastructure.Messaging.RabbitMQ.Motorbikes;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;

namespace MotoBusiness.External.Infrastructure.Messaging.Absctrations
{
    public static class MessagingDependency
	{
        public static IServiceCollection AddMessaging(
            this IServiceCollection services)
        {
            services.AddHostedService<MotorbikeConsumer>();
            services.AddScoped<IMotorbikePublisher, MotorbikePublisher>();

            return services;
        }
    }
}