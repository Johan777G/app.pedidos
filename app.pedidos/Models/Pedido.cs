using System;
using System.Collections.Generic;

namespace app.pedidos.Models;

public partial class Pedido
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public DateTime FechaPedido { get; set; }

    public decimal Total { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
