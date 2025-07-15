using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace OrderService.Messaging
{
    public class OrderCreatedPublisher
    {
        private IChannel? _channel;

        public void Initialize(IChannel channel)
        {
            _channel = channel;
        }

        public void PublishOrderCreated(object message)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            _channel?.BasicPublishAsync(
                exchange: "",
                routingKey: "order.created",
                body: body
            );
        }
    }
}