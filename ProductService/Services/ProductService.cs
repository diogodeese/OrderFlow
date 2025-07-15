using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Models;

namespace ProductService.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext _context;

        public ProductService(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public Task<List<Product>> GetAllAsync()
        {
            return _context.Products.ToListAsync();
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            return _context.Products.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> UpdateAsync(int id, Product updatedProduct)
        {
            int updatedCount = await _context.Products
                .Where(o => o.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(o => o.Name, updatedProduct.Name)
                    .SetProperty(o => o.Price, updatedProduct.Price)
                    .SetProperty(o => o.Stock, updatedProduct.Stock)
                );

            return updatedCount > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            int rowsAffected = await _context.Products.Where(o => o.Id == id).ExecuteDeleteAsync();
            return rowsAffected > 0;
        }
    }
}