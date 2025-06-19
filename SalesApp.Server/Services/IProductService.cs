using SalesApp.Server.DTOs;
using SalesApp.Server.Models;

namespace SalesApp.Server.Services;
public interface IProductService {
    Task<IEnumerable<ProductDTO>> GetAllAsync();
    Task<ProductDTO?> GetAsync(Guid id);
    Task<ProductDTO> AddAsync(CreateProductDTO dto);
    Task<ProductDTO?> UpdateAsync(Guid id, UpdateProductDTO dto);
    Task<bool> RemoveAsync(Guid id);
}
