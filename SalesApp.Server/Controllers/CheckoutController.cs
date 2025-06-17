using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesApp.Server.DTOs;
using SalesApp.Server.Services;

namespace SalesApp.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService service) {
            _checkoutService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request) {
            var result = await _checkoutService.ProcessAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
