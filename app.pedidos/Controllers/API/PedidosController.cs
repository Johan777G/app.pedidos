using app.pedidos.Models;
using app.pedidos.Repositorio;
using app.pedidos.Utilidades.Dtos.Pedidos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace app.pedidos.Controllers.API
{
    [Route("api/pedidos")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoRepositorio _pedidoRepositorio;
        public PedidosController(PedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
        }

        [HttpPost()]
        [Authorize(Roles = "Admin,Cliente")]
        public IActionResult CrearPedido(CrearPedidoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_pedidoRepositorio.CrearPedido(dto, out string errorMessage))
            {
                return Ok(new { Message = "Pedido creado con éxito." });
            }

            return BadRequest(new { Error = errorMessage });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{Id}")]
        public IActionResult ObtenerPedido(int Id)
        {
            return Ok(_pedidoRepositorio.Obtener(Id));
        }
    }
}
