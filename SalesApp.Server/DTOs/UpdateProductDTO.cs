using SalesApp.Server.Models;

namespace SalesApp.Server.DTOs;
public class UpdateProductDTO {
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public ProductCategory Category { get; set; }
}
