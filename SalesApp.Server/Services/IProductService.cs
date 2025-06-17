using SalesApp.Server.DTOs;
using SalesApp.Server.Models;

namespace SalesApp.Server.Services;
public interface IProductService {
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetAsync(Guid id);
    Task<Product> AddAsync(CreateProductDTO dto);
    Task<Product?> UpdateAsync(Guid id, UpdateProductDTO dto);
    Task<bool> RemoveAsync(Guid id);
}
