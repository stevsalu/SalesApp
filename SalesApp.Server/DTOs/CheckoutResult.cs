namespace SalesApp.Server.DTOs;
public class CheckoutResult {
    public bool IsSuccess { get; set; }
    public decimal TotalCost { get; set; }
    public decimal ChangeReturned { get; set; }
    public string? ErrorMessage { get; set; }
}
