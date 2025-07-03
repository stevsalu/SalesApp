using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SalesApp.Server.DTOs;
using SalesApp.Server.Hubs;

namespace SalesApp.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase {
    private readonly IHubContext<ProductHub> _hub;

    public CartController(IHubContext<ProductHub> hub) {
        _hub = hub;
    }

    [HttpPost("reserve")]
    public async Task<IActionResult> Reserve([FromBody] ReserveItemRequest request) {
        // Would be nice to handle the cart logic in db aswell but this will do for a simple kiosk usage
        await _hub.Clients.All.SendAsync("ProductReserved", new {
        ProductId = request.ProductId,
        Quantity = request.Quantity
        });
        return Ok();
    }
}

