using Application.Features.Cart.DTOs;
using Application.Features.Cart.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Souqy.Controllers
{
    // NOTE: The existing controllers (ProductsController, CategoriesController) use a
    // literal "api/v1/..." route rather than the ApiVersion attribute + templated route.
    // Matching that here for consistency; there is no API-versioning package wired up yet.
    [ApiController]
    [Route("api/v1/cart")]
    public class CartController : ControllerBase
    {
        private readonly CartService _service;

        public CartController(CartService service)
        {
            _service = service;
        }

        // TEMPORARY: userId is passed explicitly as a query parameter because JWT auth
        // is not wired up for these endpoints yet. Once Auth is in place this must be
        // replaced with the authenticated user's id from the token claims (and the
        // parameter removed from the public surface).
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetCart([FromQuery] Guid userId)
        {
            var items = await _service.GetCartAsync(userId);
            return Ok(items);
        }

        // TEMPORARY: see note above re: userId as a query parameter.
        [HttpPost("items")]
        [AllowAnonymous]
        public async Task<ActionResult> AddItem([FromQuery] Guid userId, [FromBody] AddToCartDto dto)
        {
            var item = await _service.AddToCartAsync(userId, dto);
            return Ok(item);
        }

        // TEMPORARY: see note above re: userId as a query parameter.
        [HttpDelete("items/{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult> RemoveItem(Guid id, [FromQuery] Guid userId)
        {
            var removed = await _service.RemoveFromCartAsync(userId, id);
            if (!removed) return NotFound();
            return NoContent();
        }
    }
}
