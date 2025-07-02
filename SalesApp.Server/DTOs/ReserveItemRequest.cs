namespace SalesApp.Server.DTOs;

public class ReserveItemRequest {
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
