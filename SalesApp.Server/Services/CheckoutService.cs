using Microsoft.EntityFrameworkCore;
using SalesApp.Server.Data;
using SalesApp.Server.DTOs;

namespace SalesApp.Server.Services;
public class CheckoutService : ICheckoutService {
    private readonly AppDbContext _context;

    public CheckoutService(AppDbContext context) {
        _context = context;
    }

    public async Task<CheckoutResult> ProcessAsync(CheckoutRequest request) {
        using var transaction = await _context.Database.BeginTransactionAsync();

        var productIds = request.Items.Select(i => i.ProductId).ToList();
        var products = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();

        decimal total = 0;

        foreach (var item in request.Items) {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId);
            if (product == null)
                return new CheckoutResult { IsSuccess = false, ErrorMessage = "Product not found." };

            if (item.Quantity <= 0)
                return new CheckoutResult { IsSuccess = false, ErrorMessage = "Invalid quantity." };

            if (product.Quantity < item.Quantity)
                return new CheckoutResult { IsSuccess = false, ErrorMessage = $"{product.Name} is out of stock." };

            total += item.Quantity * product.Price;
        }

        if (request.AmountPaid < total) return new CheckoutResult { IsSuccess = false, ErrorMessage = "Insufficient payment." };

        foreach (var item in request.Items) {
            var product = products.First(p => p.Id == item.ProductId);
            product.Quantity -= item.Quantity;
        }

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return new CheckoutResult {
            IsSuccess = true,
            TotalCost = total,
            ChangeReturned = Math.Round(request.AmountPaid - total, 2)
        };
    }
}
