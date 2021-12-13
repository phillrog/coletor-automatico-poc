using ColetaAutomatica.API.Application.Commands;
using ColetaAutomatica.API.Application.Events;
using ColetaAutomatica.API.Data;
using ColetaAutomatica.API.Data.Repository;
using ColetaAutomatica.API.Models;
using ColetaAutomatica.Core.Data.EventSourcing;
using ColetaAutomatica.Core.Mediator;
using ColetaAutomatica.TratamentoEventSourcing;
using FluentValidation.Results;
using MediatR;

namespace ColetaAutomatica.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<INotificationHandler<MonitoramentoSolicitadoEvent>, MonitoramentoSolicitadoEventHandler>();

            services.AddScoped<IRequestHandler<SolicitarMonitoramentoProcessoCommand, ValidationResult>, SolicitarMonitoramentoProcessoCommandHandler>();


            services.AddScoped<ISolicitacaoProcessoRepositoryAsync, SolicitacaoProcessoRepository>();
            services.AddScoped<ColetaAutomaticaContext>();

            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();
        }
    }
}
