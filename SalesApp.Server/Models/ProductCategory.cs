using System.ComponentModel.DataAnnotations;

namespace SalesApp.Server.Models;
public class ProductCategory {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}
