using Application.Features.Orders.DTOs;
using Application.Features.Orders.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Souqy.Controllers
{
    // NOTE: matches the literal "api/v1/..." route style of the existing controllers;
    // no API-versioning package is wired up yet.
    [ApiController]
    [Route("api/v1/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _service;

        public OrdersController(OrderService service)
        {
            _service = service;
        }

        // TEMPORARY: userId is passed explicitly as a query parameter because JWT auth
        // is not wired up for these endpoints yet. Once Auth is in place this must be
        // replaced with the authenticated user's id from the token claims (and the
        // parameter removed from the public surface).
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create([FromQuery] Guid userId, [FromBody] CreateOrderDto dto)
        {
            var order = await _service.CreateOrderAsync(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id = order.Id, userId }, order);
        }

        // TEMPORARY: see note above re: userId as a query parameter.
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetById(Guid id, [FromQuery] Guid userId)
        {
            var order = await _service.GetOrderAsync(userId, id);
            if (order is null) return NotFound();
            return Ok(order);
        }

        // TEMPORARY: see note above re: userId as a query parameter.
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll([FromQuery] Guid userId)
        {
            var orders = await _service.GetUserOrdersAsync(userId);
            return Ok(orders);
        }
    }
}
