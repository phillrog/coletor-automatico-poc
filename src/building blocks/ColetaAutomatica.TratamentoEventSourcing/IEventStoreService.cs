using EventStore.ClientAPI;

namespace ColetaAutomatica.TratamentoEventSourcing
{
    public interface IEventStoreService
    {
        IEventStoreConnection GetConnection();
    }
}
