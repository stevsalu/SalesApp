using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApp.Server.Data;
using SalesApp.Server.Services;

namespace SalesApp.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var categories = await _service.GetAllAsync();
            return Ok(categories);
        }
    }
}
