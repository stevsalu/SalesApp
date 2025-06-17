using AutoMapper;
using SalesApp.Server.DTOs;
using SalesApp.Server.Models;
using SalesApp.Server.Repositories;
using System.Collections.Concurrent;

namespace SalesApp.Server.Services;
public class ProductService : IProductService {

    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper) {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Product>> GetAllAsync() {
        var products = await _productRepository.GetAllAsync();
        return products;
    }

    public async Task<Product?> GetAsync(Guid id) {
        var product = await _productRepository.GetByIdAsync(id);
        return product is null ? null : product;
    }

    public async Task<Product> AddAsync(CreateProductDTO dto) {
        var product = _mapper.Map<Product>(dto);
        var created = await _productRepository.AddAsync(product);
        return created;
    }

    public async Task<Product?> UpdateAsync(Guid id, UpdateProductDTO dto) {
        var productToUpdate = _mapper.Map<Product>(dto);
        var updated = await _productRepository.UpdateAsync(id, productToUpdate);
        return updated == null ? null : updated;
    }

    public async Task<bool> RemoveAsync(Guid id) {
        return await _productRepository.DeleteAsync(id);
    }
}
