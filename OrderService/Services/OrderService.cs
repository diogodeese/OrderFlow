using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Messaging;
using OrderService.Models;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _context;
        private readonly OrderCreatedPublisher _publisher;

        public OrderService(OrderDbContext context, OrderCreatedPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            _publisher.PublishOrderCreated(order);

            return order;
        }

        public Task<List<Order>> GetAllAsync()
        {
            return _context.Orders.ToListAsync();
        }

        public Task<Order?> GetByIdAsync(int id)
        {
            return _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> UpdateAsync(int id, Order updatedOrder)
        {
            int updatedCount = await _context.Orders
                .Where(o => o.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(o => o.Customer, updatedOrder.Customer)
                    .SetProperty(o => o.Total, updatedOrder.Total)
                    .SetProperty(o => o.Status, updatedOrder.Status)
                );

            return updatedCount > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            int rowsAffected = await _context.Orders.Where(o => o.Id == id).ExecuteDeleteAsync();

            return rowsAffected > 0;
        }
    }
}