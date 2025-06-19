using SalesApp.Server.Models;

namespace SalesApp.Server.Repositories;

public interface ICategoryRepository {
    Task<IEnumerable<ProductCategory>> GetAllAsync();
    Task<ProductCategory?> GetByIdAsync(int id);
}
