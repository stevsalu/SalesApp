using SalesApp.Server.Models;
using SalesApp.Server.Repositories;

namespace SalesApp.Server.Services;
public class CategoryService : ICategoryService {
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository) {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<ProductCategory>> GetAllAsync() {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<ProductCategory?> GetByIdAsync(int id) {
        return await _categoryRepository.GetByIdAsync(id);
    }
}
