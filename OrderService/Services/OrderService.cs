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
            return _context.Orders.Include(o => o.Items).ToListAsync();
        }

        public Task<Order?> GetByIdAsync(int id)
        {
            return _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> UpdateAsync(int id, Order updatedOrder)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (existingOrder == null)
                return false;

            existingOrder.Customer = updatedOrder.Customer;
            existingOrder.Total = updatedOrder.Total;
            existingOrder.Status = updatedOrder.Status;

            _context.OrderItems.RemoveRange(existingOrder.Items);
            existingOrder.Items = updatedOrder.Items;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            int rowsAffected = await _context.Orders.Where(o => o.Id == id).ExecuteDeleteAsync();

            return rowsAffected > 0;
        }
    }
}