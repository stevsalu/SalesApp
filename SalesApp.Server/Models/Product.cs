using System.ComponentModel.DataAnnotations;

namespace SalesApp.Server.Models;
public class Product {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int CategoryId { get; set; }
    public ProductCategory? Category { get; set; }
}
