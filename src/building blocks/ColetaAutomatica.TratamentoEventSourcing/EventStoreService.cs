using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;

namespace ColetaAutomatica.TratamentoEventSourcing
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreConnection _connection;

        public EventStoreService(IConfiguration configuration)
        {
            var settingsBuilder = ConnectionSettings
                .Create()
                .KeepReconnecting()
                .LimitReconnectionsTo(10);
            _connection = EventStoreConnection.Create(
                configuration.GetConnectionString("EventStoreConnection"),
                settingsBuilder, "coleta_automatica_api_storeevent");

            _connection.ConnectAsync();            
        }

        public IEventStoreConnection GetConnection()
        {
            return _connection;
        }        
    }
}
