using ColetaAutomatica.ConsultaJurisprudencia.API.Data;
using ColetaAutomatica.ConsultaJurisprudencia.API.Data.Repository;
using ColetaAutomatica.ConsultaJurisprudencia.API.Models;
using ColetaAutomatica.ConsultaJurisprudencia.API.Services.Handlers;
using ColetaAutomatica.Core.Data.EventSourcing;
using ColetaAutomatica.Core.Mediator;
using ColetaAutomatica.TratamentoEventSourcing;

namespace ColetaAutomatica.ConsultaJurisprudencia.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            
            services.AddScoped<ISolicitacaoProcessoRepositoryAsync, SolicitacaoProcessoRepository>();
            services.AddScoped<ConsultaJurisprudenciaContext>();

            services.AddScoped<IMonitoramentoSolicitadoService, MonitoramentoSolicitadoService>();

            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();
        }
    }
}
