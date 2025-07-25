using System.Text;
using System.Text.Json;
using ProductService.Data;
using ProductService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProductService.Messaging
{
    public class OrderCreatedConsumer
    {
        private IChannel? _channel;
        private readonly IServiceProvider _serviceProvider;

        public OrderCreatedConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Initialize(IChannel channel)
        {
            _channel = channel;

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"📦 [ProductService] Received message: {message}");

                    var order = JsonSerializer.Deserialize<OrderCreatedEvent>(message);
                    if (order == null) return;

                    using var scope = _serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();

                    foreach (var item in order.Items)
                    {
                        var product = await db.Products.FindAsync(item.ProductId);
                        if (product != null)
                        {
                            product.Stock -= item.Quantity;
                        }
                    }

                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error handling message: {ex.Message}");
                }
            };

            _channel.BasicConsumeAsync(
                queue: "order.created",
                autoAck: true,
                consumer: consumer
            );
        }
    }
}