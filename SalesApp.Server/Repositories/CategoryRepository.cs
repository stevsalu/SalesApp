using Microsoft.EntityFrameworkCore;
using SalesApp.Server.Data;
using SalesApp.Server.Models;

namespace SalesApp.Server.Repositories;

public class CategoryRepository : ICategoryRepository {
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<ProductCategory>> GetAllAsync() {
        return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<ProductCategory?> GetByIdAsync(int id) {
        return await _context.Categories.FindAsync(id);
    }
}
