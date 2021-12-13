using ColetaAutomatica.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ColetaAutomatica.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("Localhost");

            services.AddDbContext<ColetaAutomaticaContext>(options =>
                options.UseSqlServer(conn, m => m.MigrationsAssembly("ColetaAutomatica.API")));
            
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