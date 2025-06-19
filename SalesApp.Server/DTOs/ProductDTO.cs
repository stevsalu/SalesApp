using SalesApp.Server.Models;

namespace SalesApp.Server.DTOs;
public class ProductDTO {
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = default!;
}
