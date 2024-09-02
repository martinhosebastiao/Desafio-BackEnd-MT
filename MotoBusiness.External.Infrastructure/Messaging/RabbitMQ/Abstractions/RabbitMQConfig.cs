namespace MotoBusiness.External.Infrastructure.Messaging.RabbitMQ.Abstractions
{
    public abstract class RabbitMQConfig
	{
		public string HostName { get; set; } = string.Empty;
		public string VirtualHost { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

