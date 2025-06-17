namespace SalesApp.Server.DTOs;
public class CheckoutRequest {
    public List<BasketItem> Items { get; set; } = new();
    public decimal AmountPaid { get; set; }
}
