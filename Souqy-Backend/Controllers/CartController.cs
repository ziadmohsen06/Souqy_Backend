using System.Security.Claims;
using Application.Features.Cart.DTOs;
using Application.Features.Cart.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Souqy.Controllers
{
    [ApiController]
    [Route("api/v1/cart")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly CartService _service;

        public CartController(CartService service)
        {
            _service = service;
        }

        private Guid? GetCurrentUserId()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("sub")?.Value;
            return Guid.TryParse(idClaim, out var userId) ? userId : null;
        }

        [HttpGet]
        public async Task<ActionResult> GetCart()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var items = await _service.GetCartAsync(userId.Value);
            return Ok(items);
        }

        [HttpPost("items")]
        public async Task<ActionResult> AddItem([FromBody] AddToCartDto dto)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var item = await _service.AddToCartAsync(userId.Value, dto);
            return Ok(item);
        }

        [HttpDelete("items/{id:guid}")]
        public async Task<ActionResult> RemoveItem(Guid id)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var removed = await _service.RemoveFromCartAsync(userId.Value, id);
            if (!removed) return NotFound();
            return NoContent();
        }
    }
}
