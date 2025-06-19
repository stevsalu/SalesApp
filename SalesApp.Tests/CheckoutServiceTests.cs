using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SalesApp.Server.Data;
using SalesApp.Server.DTOs;
using SalesApp.Server.Models;
using SalesApp.Server.Services;
using System.Linq;
using Xunit;

namespace SalesApp.Tests;

public class CheckoutServiceTests {

    private readonly AppDbContext _context;
    private readonly ICheckoutService _service;
    private readonly SqliteConnection _connection;

    public CheckoutServiceTests() {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new AppDbContext(options);
        _context.Database.EnsureCreated();
        var category = new ProductCategory { Id = 1, Name = "Edible" };
        _context.Products.AddRange(
            new Product { Id = Guid.NewGuid(), Name = "Brownie", Price = 1.00m, Quantity = 10, CategoryId = 1, Category = category },
            new Product { Id = Guid.NewGuid(), Name = "Muffin", Price = 2.00m, Quantity = 5, CategoryId = 1, Category = category }
        );

        _context.SaveChanges();

        _service = new CheckoutService(_context);
    }

    [Fact]
    public async Task Checkout_ShouldSucceed_WithMultipleItems() {
        var products = _context.Products.ToList();

        var brownie = _context.Products.First(p => p.Name == "Brownie");
        var muffin = _context.Products.First(p => p.Name == "Muffin");

        var request = new CheckoutRequest {
            AmountPaid = 20m,
            Items = new List<BasketItem>
            {
                new BasketItem { ProductId = brownie.Id, Quantity = 3 }, // 3 x 1 = 3
                new BasketItem { ProductId = muffin.Id, Quantity = 2 }  // 2 x 2 = 4
            }
        };

        var result = await _service.ProcessAsync(request);

        Assert.True(result.IsSuccess);
        Assert.Equal(7.00m, result.TotalCost);
        Assert.Equal(13.00m, result.ChangeReturned);

        var updated1 = _context.Products.First(p => p.Id == brownie.Id);
        var updated2 = _context.Products.First(p => p.Id == muffin.Id);

        Assert.Equal(7, updated1.Quantity);
        Assert.Equal(3, updated2.Quantity);
    }

    [Fact]
    public async Task Checkout_ShouldSucceed_WhenValid() {
        var product = _context.Products.First();
        var brownie = _context.Products.First(p => p.Name == "Brownie");

        var request = new CheckoutRequest {
            AmountPaid = 10m,
            Items = new List<BasketItem> {
                    new BasketItem { ProductId = brownie.Id, Quantity = 2 }
            }
        };

        var result = await _service.ProcessAsync(request);

        Assert.True(result.IsSuccess);
        Assert.Equal(8m, result.ChangeReturned);
        Assert.Equal(8, _context.Products.First(p => p.Id == brownie.Id).Quantity); // 10 - 2
    }

    [Fact]
    public async Task Checkout_ShouldFail_WhenOutOfStock() {
        var product = _context.Products.First();

        var request = new CheckoutRequest {
            AmountPaid = 100m,
            Items = new List<BasketItem>
            {
                    new BasketItem { ProductId = product.Id, Quantity = 999 }
                }
        };

        var result = await _service.ProcessAsync(request);

        Assert.False(result.IsSuccess);
        Assert.Contains("out of stock", result.ErrorMessage);
    }

    [Fact]
    public async Task Checkout_ShouldFail_WhenInsufficientCash() {
        var product = _context.Products.First();

        var request = new CheckoutRequest {
            AmountPaid = 1m,
            Items = new List<BasketItem>
            {
                    new BasketItem { ProductId = product.Id, Quantity = 5 }
                }
        };

        var result = await _service.ProcessAsync(request);

        Assert.False(result.IsSuccess);
        Assert.Contains("Insufficient payment", result.ErrorMessage);
    }

    [Fact]
    public async Task Checkout_ShouldFail_WhenInvalidQuantity() {
        var product = _context.Products.First();

        var request = new CheckoutRequest {
            AmountPaid = 10m,
            Items = new List<BasketItem>
            {
                    new BasketItem { ProductId = product.Id, Quantity = 0 }
                }
        };

        var result = await _service.ProcessAsync(request);

        Assert.False(result.IsSuccess);
        Assert.Contains("Invalid quantity", result.ErrorMessage);
    }

    public void Dispose() {
        _context.Dispose();
        _connection.Dispose();
    }
}
