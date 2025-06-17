using AutoMapper;
using Moq;
using SalesApp.Server.DTOs;
using SalesApp.Server.Mapper;
using SalesApp.Server.Models;
using SalesApp.Server.Repositories;
using SalesApp.Server.Services;

namespace SalesApp.Tests;
public class ProductServiceTests {
    private readonly IProductService _service;
    private readonly Mock<IProductRepository> _mockRepo = new();
    private readonly IMapper _mapper;

    public ProductServiceTests() {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
        _service = new ProductService(_mockRepo.Object, _mapper);
    }

    [Fact]
    public async Task AddAsync_ShouldAddProduct() {
        var dto = new CreateProductDTO {
            Name = "Test",
            Price = 1.23m,
            Quantity = 10,
            Category = ProductCategory.Edible
        };

        var createdProduct = _mapper.Map<Product>(dto);

        _mockRepo.Setup(r => r.AddAsync(It.IsAny<Product>()))
                 .ReturnsAsync(createdProduct);

        var result = await _service.AddAsync(dto);

        Assert.Equal(dto.Name, result.Name);
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProducts() {
        var products = new List<Product>
        {
                new Product { Name = "A", Price = 1, Quantity = 1, Category = ProductCategory.Edible },
                new Product { Name = "B", Price = 2, Quantity = 2, Category = ProductCategory.Clothing }
            };

        _mockRepo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(products);

        var result = await _service.GetAllAsync();

        Assert.Equal(2, ((List<Product>)result).Count);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnProduct_WhenExists() {
        var id = Guid.NewGuid();
        var product = new Product {
            Id = id,
            Name = "Single",
            Price = 2.5m,
            Quantity = 10,
            Category = ProductCategory.Edible
        };

        _mockRepo.Setup(r => r.GetByIdAsync(id))
                 .ReturnsAsync(product);

        var result = await _service.GetAsync(id);

        Assert.NotNull(result);
        Assert.Equal(id, result!.Id);
    }

    [Fact]
    public async Task RemoveAsync_ShouldReturnTrue_WhenDeleted() {
        var id = Guid.NewGuid();

        _mockRepo.Setup(r => r.DeleteAsync(id))
                 .ReturnsAsync(true);

        var result = await _service.RemoveAsync(id);

        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedProduct() {
        var id = Guid.NewGuid();
        var dto = new UpdateProductDTO {
            Name = "Updated",
            Price = 2.5m,
            Quantity = 50,
            Category = ProductCategory.Clothing
        };

        var product = _mapper.Map<Product>(dto);

        var updatedProduct = new Product {
            Id = id,
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity,
            Category = dto.Category
        };

        _mockRepo.Setup(r => r.UpdateAsync(id, product))
                 .ReturnsAsync(updatedProduct);

        var result = await _service.UpdateAsync(id, dto);

        Assert.NotNull(result);
        Assert.Equal("Updated", result!.Name);
        Assert.Equal(ProductCategory.Clothing, result.Category);
    }
}
