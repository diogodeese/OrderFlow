using ProductService.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace ProductService.Messaging
{
    class RabbitMqHostedService : IHostedService
    {
        private readonly OrderCreatedConsumer _consumer;
        private IConnection? _connection;
        private IChannel? _channel;

        public RabbitMqHostedService(OrderCreatedConsumer consumer)
        {
            _consumer = consumer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                UserName = "guest",
                Password = "guest"
            };

            int retries = 10;
            int delaySeconds = 5;

            for (int i = 0; i < retries; i++)
            {
                try
                {
                    _connection = await factory.CreateConnectionAsync();
                    _channel = await _connection.CreateChannelAsync();

                    await _channel.QueueDeclareAsync(
                        queue: "order.created",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null,
                        cancellationToken: cancellationToken
                    );

                    _consumer.Initialize(_channel);
                    Console.WriteLine("✅ Connected to RabbitMQ.");
                    return;
                }
                catch (BrokerUnreachableException)
                {
                    Console.WriteLine($"⏳ RabbitMQ not ready. Retry {i + 1}/{retries} in {delaySeconds}s...");
                    await Task.Delay(TimeSpan.FromSeconds(delaySeconds), cancellationToken);
                }
            }

            throw new Exception("❌ Failed to connect to RabbitMQ after multiple retries.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
            return Task.CompletedTask;
        }
    }
}