using CRUD_Cliente2.Web.Models;

namespace CRUD_Cliente2.Web.Strategy
{
    public interface IClienteStrategy
    {
        Task ExecutarAsync(Cliente cliente, CancellationToken cancellationToken);
    }
}
