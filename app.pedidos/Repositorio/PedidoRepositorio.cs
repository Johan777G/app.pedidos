using app.pedidos.Models;
using app.pedidos.Utilidades.Dtos.Pedidos;

namespace app.pedidos.Repositorio
{
    public class PedidoRepositorio
    {
        private readonly PedidosContext _context;

        public PedidoRepositorio(PedidosContext context)
        {
            _context = context;
        }

        public Pedido? Obtener(int Id)
        {
            return _context.Pedidos.FirstOrDefault(x => x.Id == Id);
        }

        public bool CrearPedido(CrearPedidoDto pedidoDto, out string errorMessage)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Validar existencia del cliente
                var cliente = _context.Clientes.Find(pedidoDto.ClienteId);
                if (cliente == null)
                {
                    errorMessage = "El cliente no existe.";
                    return false;
                }

                // Crear el pedido
                var nuevoPedido = new Pedido
                {
                    ClienteId = pedidoDto.ClienteId,
                    FechaPedido = DateTime.Now,
                    Total = pedidoDto.Detalles.Sum(x => x.Cantidad * x.Precio),
                };
                _context.Pedidos.Add(nuevoPedido);
                _context.SaveChanges();

                decimal total = 0;

                // Procesar los detalles del pedido
                foreach (var detalleDto in pedidoDto.Detalles)
                {
                    var producto = _context.Productos.Find(detalleDto.ProductoId);
                    if (producto == null)
                    {
                        errorMessage = $"El producto con ID {detalleDto.ProductoId} no existe.";
                        transaction.Rollback();
                        return false;
                    }

                    if (producto.Stock < detalleDto.Cantidad)
                    {
                        errorMessage = $"El producto {producto.Nombre} no tiene stock suficiente.";
                        transaction.Rollback();
                        return false;
                    }

                    // Reducir stock
                    producto.Stock -= detalleDto.Cantidad;
                    _context.Productos.Update(producto);

                    // Calcular subtotal y total
                    var subtotal = producto.Precio * detalleDto.Cantidad;
                    total += subtotal;

                    // Crear detalle del pedido
                    var detallePedido = new PedidoProducto
                    {
                        PedidoId = nuevoPedido.Id,
                        ProductoId = detalleDto.ProductoId,
                        Cantidad = detalleDto.Cantidad,
                    };
                    _context.PedidoProductos.Add(detallePedido);
                }

                // Guardar el total del pedido
                nuevoPedido.Total = total;
                _context.Pedidos.Update(nuevoPedido);

                _context.SaveChanges();
                transaction.Commit();

                errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                errorMessage = $"Error al crear el pedido: {ex.Message}";
                return false;
            }
        }
    }
}
