using Microsoft.EntityFrameworkCore;
using SalesApp.Server.Data;
using SalesApp.Server.DTOs;
using SalesApp.Server.Models;

namespace SalesApp.Server.Repositories;
public class ProductRepository : IProductRepository {
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync() {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id) {
        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> AddAsync(Product product) {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateAsync(Guid id, Product updatedProduct) {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return null;

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        product.Quantity = updatedProduct.Quantity;
        product.Category = updatedProduct.Category;

        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteAsync(Guid id) {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}

