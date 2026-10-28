using Microsoft.AspNetCore.Mvc;
using Tienda_don_Pedro.DTOs;
using Tienda_don_Pedro.Services;

namespace Tienda_don_Pedro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductosController(IProductoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoDto>>> Get()
        {
            var productos = await _service.ObtenerTodos();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> Get(int id)
        {
            var producto = await _service.ObtenerPorId(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDto>> Post([FromBody] CrearProductoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var producto = await _service.Cr Crear(dto);
            return CreatedAtAction(nameof(Get), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ActualizarProductoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var resultado = await _service.Actualizar(id, dto);
            if (!resultado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _service.Eliminar(id);
            if (!resultado) return NotFound();
            return NoContent();
        }
    }
}
