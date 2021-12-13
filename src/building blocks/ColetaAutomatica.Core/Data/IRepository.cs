using ColetaAutomatica.Core.DomainObjects;

namespace ColetaAutomatica.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}