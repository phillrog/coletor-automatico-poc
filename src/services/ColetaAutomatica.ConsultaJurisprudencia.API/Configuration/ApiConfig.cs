using ColetaAutomatica.ConsultaJurisprudencia.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ColetaAutomatica.ConsultaJurisprudencia.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("Localhost");

            services.AddDbContext<ConsultaJurisprudenciaContext>(options =>
                options.UseSqlServer(conn));
            
            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });
        }
    }
}