using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MotoBusiness.External.Infrastructure.Messaging.Absctrations;
using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MotoBusiness.External.Infrastructure.Messaging.RabbitMQ.Motorbikes
{
    public sealed class MotorbikeConsumer : BackgroundService
    {
        private readonly IModel _channel;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MotorbikeConsumer> _logger;

        public MotorbikeConsumer(
            IUnitOfWork unitOfWork,
            IConfiguration configuration,
            ILogger<MotorbikeConsumer> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _channel = new BaseRabbitMQ(
                Queues.MotorbikeRegister, configuration)._channel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, b) =>
            {
                var body = b.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var motorbike = JsonSerializer.Deserialize<Motorbike>(message);

                if (motorbike is null)
                {
                    return;
                }

                if (motorbike.IsManufactureYears2024())
                {
                    if (motorbike.IsValid)
                    {
                        await _unitOfWork.MotorbikeRepository.CreateAsync(
                                              motorbike, stoppingToken);

                        await _unitOfWork.CommitAsync(stoppingToken);
                    }
                    else
                    {
                        foreach (var erro in motorbike.Errors)
                        {
                            var _message = string.Concat(
                                "MotorbikeConsumer", erro);

                            _logger.LogError(_message);
                        }

                        await _unitOfWork.RollbackAsync(stoppingToken);
                    }
                }
                else
                {
                    _logger.LogInformation(
                        MotorbikeErrors.DiffYear(motorbike.Year).Description);
                }
            };

            _channel.BasicConsume(Queues.MotorbikeRegister, true, consumer);

            await Task.CompletedTask;
        }
    }
}