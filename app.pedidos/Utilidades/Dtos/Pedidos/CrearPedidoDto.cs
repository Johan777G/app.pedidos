using System.ComponentModel.DataAnnotations;

namespace app.pedidos.Utilidades.Dtos.Pedidos
{
    public class CrearPedidoDto
    {
        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Debe incluir al menos un producto.")]
        public List<DetallePedidoDto> Detalles { get; set; } = new List<DetallePedidoDto>();
    }

    public class DetallePedidoDto
    {
        [Required(ErrorMessage = "El ID del producto es obligatorio.")]
        public int ProductoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }

        public decimal Precio { get; set; }
    }
}
