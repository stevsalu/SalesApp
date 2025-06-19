using SalesApp.Server.Models;

namespace SalesApp.Server.DTOs;
public class CreateProductDTO {
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 0;
    public int CategoryId { get; set; }
}