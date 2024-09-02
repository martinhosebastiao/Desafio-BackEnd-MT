using Microsoft.Extensions.Configuration;
using MotoBusiness.External.Infrastructure.Messaging.RabbitMQ.Abstractions;
using RabbitMQ.Client;

namespace MotoBusiness.External.Infrastructure.Messaging.RabbitMQ
{
    public class BaseRabbitMQ
    {
        public readonly IModel _channel;
        private readonly IConnection _connection;

        public BaseRabbitMQ(string queueName, IConfiguration configuration)
        {
            var config = configuration.GetValue<RabbitMQConfig>("RabbitMQConfig");
            var factory = new ConnectionFactory()
            {
                HostName = config?.HostName,
                VirtualHost = config?.VirtualHost,
                UserName = config?.UserName,
                Password = config?.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }
    }
}

