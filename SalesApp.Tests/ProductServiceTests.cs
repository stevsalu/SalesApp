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
            CategoryId = 1
        };

        var createdProduct = new Product {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity,
            CategoryId = dto.CategoryId,
            Category = new ProductCategory { Id = dto.CategoryId, Name = "Edible" }
        };

        _mockRepo.Setup(r => r.AddAsync(It.IsAny<Product>()))
                 .ReturnsAsync(createdProduct);

        var result = await _service.AddAsync(dto);

        Assert.Equal(dto.Name, result.Name);
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProducts() {
        var products = new List<Product> {
            new Product {
                Name = "A",
                Price = 1,
                Quantity = 1,
                CategoryId = 1,
                Category = new ProductCategory { Id = 1, Name = "Edible" }
            },
            new Product
            {
                Name = "B",
                Price = 2,
                Quantity = 2,
                CategoryId = 2,
                Category = new ProductCategory { Id = 2, Name = "Clothing" }
            }
        };

        _mockRepo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(products);

        var result = await _service.GetAllAsync();

        Assert.Equal(2, ((List<ProductDTO>)result).Count);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnProduct_WhenExists() {
        var id = Guid.NewGuid();
        var product = new Product {
            Id = id,
            Name = "Single",
            Price = 2.5m,
            Quantity = 10,
            CategoryId = 1,
            Category = new ProductCategory { Id = 1, Name = "Edible" }
        };

        _mockRepo.Setup(r => r.GetByIdAsync(id))
                 .ReturnsAsync(product);

        var result = await _service.GetAsync(id);

        Assert.NotNull(result);
        Assert.Equal(id, result!.Id);
        Assert.Equal("Edible", result.CategoryName);
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
            CategoryId = 2
        };

        var updatedProduct = new Product {
            Id = id,
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity,
            CategoryId = 2,
            Category = new ProductCategory { Id = 2, Name = "Clothing" }
        };

        _mockRepo.Setup(r => r.UpdateAsync(id, It.IsAny<Product>()))
                 .ReturnsAsync(updatedProduct);

        var result = await _service.UpdateAsync(id, dto);

        Assert.NotNull(result);
        Assert.Equal("Updated", result!.Name);
        Assert.Equal(2, result.CategoryId);
        Assert.Equal("Clothing", result.CategoryName);
    }
}
