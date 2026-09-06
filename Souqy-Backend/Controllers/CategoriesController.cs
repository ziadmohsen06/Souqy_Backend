using Microsoft.AspNetCore.Mvc;
using Application.Features.Categories.Services;
using Application.Features.Categories.DTOs;

namespace Souqy.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _service;

        public CategoriesController(CategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(CancellationToken ct = default)
        {
            var items = await _service.GetAllAsync(ct);
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id, CancellationToken ct = default)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            if (dto is null) return NotFound();
            return Ok(dto);
        }
    }
}
