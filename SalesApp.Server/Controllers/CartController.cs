using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SalesApp.Server.DTOs;
using SalesApp.Server.Hubs;

namespace SalesApp.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase {
    private readonly IHubContext<ProductHub> _hub;

    [HttpPost("reserve")]
    public async Task<IActionResult> Reserve([FromBody] ReserveItemRequest request) {
        await _hub.Clients.All.SendAsync("ProductReserved");
        return Ok();
    }
}

