using SalesApp.Server.Models;

namespace SalesApp.Server.Services;

public interface ICategoryService {
    Task<IEnumerable<ProductCategory>> GetAllAsync();
    Task<ProductCategory?> GetByIdAsync(int id);
}