using Microsoft.EntityFrameworkCore;
using SalesApp.Server.Data;
using SalesApp.Server.Models;

namespace SalesApp.Server;
public class DataSeeder {
    public static async Task SeedAsync(AppDbContext context) {

        if (!await context.Categories.AnyAsync()) {
            var categories = new[]
            {
            new ProductCategory { Name = "Edible" },
            new ProductCategory { Name = "Clothing" }
        };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        if (await context.Products.AnyAsync()) return;

        var edibleId = context.Categories.First(c => c.Name == "Edible").Id;
        var clothingId = context.Categories.First(c => c.Name == "Clothing").Id;

        var items = new[]
        {
                new Product { Name = "Brownie", Price = 0.65m, Quantity = 48, CategoryId = edibleId },
                new Product { Name = "Muffin", Price = 1.00m, Quantity = 36, CategoryId = edibleId },
                new Product { Name = "Cake Pop", Price = 1.35m, Quantity = 24, CategoryId = edibleId },
                new Product { Name = "Apple tart", Price = 1.50m, Quantity = 60, CategoryId = edibleId },
                new Product { Name = "Water", Price = 1.50m, Quantity = 30, CategoryId = edibleId },
                new Product { Name = "Shirt", Price = 2.00m, Quantity = 0, CategoryId = clothingId },
                new Product { Name = "Pants", Price = 3.00m, Quantity = 0, CategoryId = clothingId },
                new Product { Name = "Jacket", Price = 4.00m, Quantity = 0, CategoryId = clothingId },
                new Product { Name = "Toy", Price = 1.00m, Quantity = 0, CategoryId = clothingId },
            };

        context.Products.AddRange(items);
        await context.SaveChangesAsync();
    }
}

