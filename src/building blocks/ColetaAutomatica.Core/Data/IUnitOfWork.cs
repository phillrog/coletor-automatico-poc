using System.Threading.Tasks;

namespace ColetaAutomatica.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}