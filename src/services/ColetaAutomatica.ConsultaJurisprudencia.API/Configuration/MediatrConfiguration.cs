using MediatR;

namespace ColetaAutomatica.ConsultaJurisprudencia.API.Configuration
{
    public static class MediatrConfiguration
    {
        public static void AddMediatRApi(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Program));
        }
    }
}
