using SalesApp.Server.DTOs;
using SalesApp.Server.Models;

namespace SalesApp.Server.Repositories;
public interface IProductRepository {
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product> AddAsync(Product product);
    Task<Product?> UpdateAsync(Guid id, Product product);
    Task<bool> DeleteAsync(Guid id);
}

