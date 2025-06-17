using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesApp.Server.DTOs;
using SalesApp.Server.Models;
using SalesApp.Server.Services;

namespace SalesApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase {
    private readonly IProductService _productService;

    public ProductController(IProductService service) {
        _productService = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        return Ok(await _productService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) {
        var product = await _productService.GetAsync(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateProductDTO dto) {
        var product = await _productService.AddAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDTO dto) {
        var updated = await _productService.UpdateAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) {
        return await _productService.RemoveAsync(id) ? NoContent() : NotFound();
    }
}

