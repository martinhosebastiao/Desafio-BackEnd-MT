using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MotoBusiness.External.Infrastructure.Messaging.Absctrations;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.External.Infrastructure.Messaging.RabbitMQ.Motorbikes
{
    public sealed class MotorbikePublisher : BaseRabbitMQ, IMotorbikePublisher
    {
        public MotorbikePublisher(
            IConfiguration configuration,
            string queueName = Queues.MotorbikeRegister) :
            base(queueName, configuration)
        {
        }

        public async Task<CustomResult> RegisterAsync(
            Motorbike motorbike, CancellationToken cancellationToken = default)
        {
            try
            {
                var json = JsonSerializer.Serialize(motorbike);
                var message = Encoding.UTF8.GetBytes(json);

                _channel.BasicPublish(
                    exchange: "",
                    routingKey: Queues.MotorbikeRegister,
                    mandatory: true,
                    basicProperties: null,
                    body: message);

                await Task.CompletedTask;

                return CustomResult.Ok(Result.Success());
            }
            catch (Exception ex)
            {
                var message = ex?.Message ?? ex?.InnerException?.Message;

                return CustomResult.Exception(
                    new Error("RegisterAsync", message));
            }
        }
    }
}

