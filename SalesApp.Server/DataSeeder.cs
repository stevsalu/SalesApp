using Microsoft.EntityFrameworkCore;
using SalesApp.Server.Data;
using SalesApp.Server.Models;

namespace SalesApp.Server;
public class DataSeeder {
    public static async Task SeedAsync(AppDbContext context) {
        if (await context.Products.AnyAsync()) return;

        var items = new[]
        {
                new Product { Name = "Brownie", Price = 0.65m, Quantity = 48, Category = ProductCategory.Edible },
                new Product { Name = "Muffin", Price = 1.00m, Quantity = 36, Category = ProductCategory.Edible },
                new Product { Name = "Cake Pop", Price = 1.35m, Quantity = 24, Category = ProductCategory.Edible },
                new Product { Name = "Apple tart", Price = 1.50m, Quantity = 60, Category = ProductCategory.Edible },
                new Product { Name = "Water", Price = 1.50m, Quantity = 30, Category = ProductCategory.Edible },
                new Product { Name = "Shirt", Price = 2.00m, Quantity = 0, Category = ProductCategory.Clothing },
                new Product { Name = "Pants", Price = 3.00m, Quantity = 0, Category = ProductCategory.Clothing },
                new Product { Name = "Jacket", Price = 4.00m, Quantity = 0, Category = ProductCategory.Clothing },
                new Product { Name = "Toy", Price = 1.00m, Quantity = 0, Category = ProductCategory.Clothing },
            };

        context.Products.AddRange(items);
        await context.SaveChangesAsync();
    }
}

