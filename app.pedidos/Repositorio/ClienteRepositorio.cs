using app.pedidos.Models;
using app.pedidos.Utilidades.Dtos.Clientes;

namespace app.pedidos.Repositorio
{
    public class ClienteRepositorio
    {
        private readonly PedidosContext _context;

        public ClienteRepositorio(PedidosContext context)
        {
            _context = context;
        }

        public bool CrearCliente(AgregarClienteDto dto)
        {
            if(EmailExiste(dto.Email))
            {
                return false; // El email ya está en uso
            }

            var nuevoCliente = new Cliente
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                FechaRegistro = DateTime.Now // Agrega la fecha de registro automáticamente
            };

            _context.Clientes.Add(nuevoCliente);
            _context.SaveChanges();
            return true;
        }
        public bool EmailExiste(string email)
        {
            return _context.Clientes.Any(c => c.Email == email);
        }
    }
}
