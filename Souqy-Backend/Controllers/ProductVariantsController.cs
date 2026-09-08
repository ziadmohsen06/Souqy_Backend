using Application.Features.Products.DTOs;
using Application.Features.Products.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Souqy.Controllers
{
    [ApiController]
    [Route("api/v1/products/{productId:guid}/variants")]
    public class ProductVariantsController : ControllerBase
    {
        private readonly ProductVariantService _service;

        public ProductVariantsController(ProductVariantService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetVariants(Guid productId, CancellationToken ct = default)
        {
            var variants = await _service.GetVariantsAsync(productId, ct);
            return Ok(variants);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateVariant(Guid productId, [FromBody] CreateProductVariantDto dto, CancellationToken ct = default)
        {
            var created = await _service.CreateVariantAsync(productId, dto, ct);
            return CreatedAtAction(nameof(GetVariants), new { productId }, created);
        }

        [HttpDelete("{variantId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteVariant(Guid productId, Guid variantId, CancellationToken ct = default)
        {
            var deleted = await _service.DeleteVariantAsync(productId, variantId, ct);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
