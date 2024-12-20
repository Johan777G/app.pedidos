using System.ComponentModel.DataAnnotations;

namespace app.pedidos.Utilidades.Dtos.Clientes
{
    public class AgregarClienteDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }
    }
}
