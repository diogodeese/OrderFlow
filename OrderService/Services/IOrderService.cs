using OrderService.Models;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<bool> UpdateAsync(int id, Order updatedOrder);
        Task<bool> DeleteAsync(int id);
    }
}