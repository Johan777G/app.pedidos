using app.pedidos.Models;
using app.pedidos.Repositorio;

namespace app.pedidos.Utilidades.Dependencias
{
    public static class InyeccionDependencias
    {
        public static IServiceCollection AddDependencias(this IServiceCollection services)
        {
            services.AddScoped<PedidosContext>();
            services.AddScoped<ClienteRepositorio>();
            services.AddScoped<PedidoRepositorio>();
            services.AddScoped<ProductoRepositorio>();

            return services;
        }
    }
}
