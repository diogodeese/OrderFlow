using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _context;

        public OrderService(OrderDbContext context)
        {
            _context = context;
        }

        public Task<List<Order>> GetAllAsync()
        {
            return _context.Orders.ToListAsync();
        }

        public Task<Order?> GetByIdAsync(int id)
        {
            return _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> UpdateAsync(int id, Order updatedOrder)
        {
            var updatedCount = await _context.Orders
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
            var rowsAffected = await _context.Orders
                .Where(o => o.Id == id)
                .ExecuteDeleteAsync();

            return rowsAffected > 0;
        }
    }
}