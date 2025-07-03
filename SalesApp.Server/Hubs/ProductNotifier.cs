using Microsoft.AspNetCore.SignalR;
using SalesApp.Server.DTOs;

namespace SalesApp.Server.Hubs;

public class ProductNotifier : IProductNotifier {
    private readonly IHubContext<ProductHub> _hubContext;

    public ProductNotifier(IHubContext<ProductHub> hubContext) {
        _hubContext = hubContext;
    }

    public Task NotifyProductUpdated(ProductDTO dto) {
        return _hubContext.Clients.All.SendAsync("ProductUpdated", dto);
    }

}

public interface IProductNotifier {
    Task NotifyProductUpdated(ProductDTO dto);
}
