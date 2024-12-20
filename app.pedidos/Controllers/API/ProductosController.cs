using app.pedidos.Repositorio;
using app.pedidos.Utilidades.Dtos.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace app.pedidos.Controllers.API
{
    [Route("api/productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoRepositorio _productoRepositorio;

        public ProductosController(ProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Cliente")]
        public IActionResult ObtenerProductos()
        {
            return Ok(_productoRepositorio.ObtenerProductos());
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult ModificarStock(int id, ModificarProductoDto dto)
        {
            var result = _productoRepositorio.ModificarProducto(id, dto);
            if (!result)
            {
                return NotFound(); // Producto no encontrado
            }
            return Ok(result);
        }

    }
}
