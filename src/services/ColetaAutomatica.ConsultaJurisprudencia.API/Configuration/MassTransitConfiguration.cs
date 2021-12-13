using MassTransit;
using MassTransit.Topology;
using Newtonsoft.Json;
using GreenPipes;
using RabbitMQ.Client;
using ColetaAutomatica.ConsultaJurisprudencia.API.Configuration;
using ColetaAutomatica.ConsultaJurisprudencia.API.EventBus.Consumer;
using ColetaAutomatica.Core.Messages.CommomMessages.IntegrationEvents;

namespace ColetaAutomatica.ConsultaJurisprudencia.API.Configuration
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
