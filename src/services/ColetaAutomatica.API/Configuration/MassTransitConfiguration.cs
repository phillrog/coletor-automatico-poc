using ColetaAutomatica.API.Application.Events;
using ColetaAutomatica.API.Configuration;
using ColetaAutomatica.API.EventBus.Consumer;
using MassTransit;
using MassTransit.Topology;
using Newtonsoft.Json;
using GreenPipes;
using RabbitMQ.Client;
using ColetaAutomatica.Core.Messages.CommomMessages.IntegrationEvents;

namespace ColetaAutomatica.API.Configuration
{
    public static class MassTransitConfiguration
    {
        public static void AddMassTransitApi(this IServiceCollection services, ConfigurationManager configuration)
        {
            var busSettings = new AppSettingsBus();
            configuration.GetSection("AppSettingsBus").Bind(busSettings);

            services.AddMassTransit(bus =>
            {
                bus.SetKebabCaseEndpointNameFormatter();
                bus.AddConsumer<MonitoramentoSolicitadoConsumer>();

                bus.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["RabbitMq:HostAddress"]);
                    
                    //cfg.ConfigureJsonSerializer(jsonSettings =>
                    //{
                    //    jsonSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    //    return jsonSettings;
                    //});

                    //cfg.Send<IMonitoramentoSolicitadoEvent>(e =>
                    //{
                    //    e.UseRoutingKeyFormatter(context => context.Message.ToString()); 
                    //});
                    //cfg.Message<IMonitoramentoSolicitadoEvent>(e => e.SetEntityName("monitoramento-solicitado-listener")); // name of exchange
                    //cfg.Publish<IMonitoramentoSolicitadoEvent>(e => e.ExchangeType = ExchangeType.Direct); // exchange type
                    //var nameFormatter = new BusEnvironmentNameFormatter(cfg.MessageTopology.EntityNameFormatter, busSettings);
                    
                    cfg.Durable = true;
                    cfg.AutoDelete = false;
                    cfg.ReceiveEndpoint(busSettings.Endpoint, opt =>
                    {
                        opt.PrefetchCount = busSettings.MessagePrefetchCount;
                        opt.UseMessageRetry(x => x.Interval(busSettings.MessageRetryCount, busSettings.MessageRetryInterval));
                        opt.UseInMemoryOutbox();
                        opt.ConfigureConsumer<MonitoramentoSolicitadoConsumer>(ctx);
                        opt.Bind("monitoramento-solicitado-listener", s =>
                        {
                            s.ExchangeType = ExchangeType.Direct;
                        });
                        
                    });

                    //cfg.MessageTopology.SetEntityNameFormatter(nameFormatter);
                });
            });
            services.AddMassTransitHostedService(true);
        }
    }

    public class BusEnvironmentNameFormatter : IEntityNameFormatter
    {
        private readonly IEntityNameFormatter _original;
        private readonly string _prefix;

        public BusEnvironmentNameFormatter(IEntityNameFormatter original, AppSettingsBus busSettings)
        {
            _original = original;
            _prefix = string.IsNullOrWhiteSpace(busSettings.Environment)
                ? string.Empty // no prefix
                : $"{busSettings.Environment}:"; // custom prefix
        }

        // Used to rename the exchanges
        public string FormatEntityName<T>()
        {
            var original = _original.FormatEntityName<T>();
            return Format(original);
        }

        // Use this one to rename the queue
        public string Format(string original)
        {
            return string.IsNullOrWhiteSpace(_prefix)
                ? original
                : $"{_prefix}{original}";
        }
    }
}
