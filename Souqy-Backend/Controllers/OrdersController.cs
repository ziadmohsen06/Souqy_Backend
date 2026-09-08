using System.Security.Claims;
using Application.Features.Orders.DTOs;
using Application.Features.Orders.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Souqy.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _service;

        public OrdersController(OrderService service)
        {
            _service = service;
        }

        private Guid? GetCurrentUserId()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("sub")?.Value;
            return Guid.TryParse(idClaim, out var userId) ? userId : null;
        }

        [HttpPost]
        [EnableRateLimiting("checkout")]
        public async Task<ActionResult> Create([FromBody] CreateOrderDto dto)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var order = await _service.CreateOrderAsync(userId.Value, dto);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var order = await _service.GetOrderAsync(userId.Value, id);
            if (order is null) return NotFound();
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var orders = await _service.GetUserOrdersAsync(userId.Value);
            return Ok(orders);
        }
    }
}
