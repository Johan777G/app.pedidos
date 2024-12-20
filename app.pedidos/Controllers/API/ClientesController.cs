using app.pedidos.Repositorio;
using app.pedidos.Utilidades.Dtos.Clientes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace app.pedidos.Controllers.API
{
    [Route("api/clientes")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteRepositorio _clienteRepositorio;

        public ClientesController(ClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        [HttpPost]
        public IActionResult CrearCliente(AgregarClienteDto dto)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("No son validos los datos enviados."); // Si el modelo no es válido, vuelve a mostrar el formulario
            }

            var resultado = _clienteRepositorio.CrearCliente(dto);
            if (!resultado)
            {
                throw new Exception("El email ya está en uso."); // Si el email no es único, vuelve al formulario con el error
            }
            return Ok(resultado);
        }
    }
}
