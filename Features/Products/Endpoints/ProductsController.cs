using Microsoft.AspNetCore.Mvc;

namespace Souqy.Features.Products.Endpoints
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
        {
            var result = await _service.GetPagedAsync(page <= 0 ? 1 : page, pageSize <= 0 ? 20 : pageSize, ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id, CancellationToken ct = default)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProductDto input, CancellationToken ct = default)
        {
            var created = await _service.CreateAsync(input, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateProductDto update, CancellationToken ct = default)
        {
            await _service.UpdateAsync(id, update, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            await _service.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
