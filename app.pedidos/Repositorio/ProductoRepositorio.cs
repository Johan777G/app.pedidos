using app.pedidos.Models;
using app.pedidos.Utilidades.Dtos.Productos;
using Microsoft.EntityFrameworkCore;

namespace app.pedidos.Repositorio
{
    public class ProductoRepositorio
    {
        private readonly PedidosContext _context;

        public ProductoRepositorio(PedidosContext pedidosContext)
        {
            _context = pedidosContext;
        }

        public List<Producto> ObtenerProductos()
        {
            return _context.Productos.ToList();
        }

        // Método para actualizar el precio y el stock de un producto
        public bool ModificarProducto(int id, ModificarProductoDto dto)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return false; // Producto no encontrado
            }

            producto.Precio = dto.Precio;
            producto.Stock = dto.Stock;

            _context.SaveChanges(); // Guarda los cambios en la base de datos
            return true; // Indica que la actualización fue exitosa
        }
    }
}
