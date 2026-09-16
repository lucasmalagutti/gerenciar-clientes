using CRUD_Cliente2.Web.Data;
using CRUD_Cliente2.Web.Models;

namespace CRUD_Cliente2.Web.Strategy
{
    public class InativarClienteStrategy : IClienteStrategy
    {
        private readonly IClienteDAO _clienteDAO;
        private readonly AppDbContext _context;

        public InativarClienteStrategy(IClienteDAO clienteDAO, AppDbContext context)
        {
            _clienteDAO = clienteDAO;
            _context = context;
        }

        public async Task ExecutarAsync(Cliente cliente, CancellationToken cancellationToken = default)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {

                    if (cliente == null || cliente.Id == 0)
                        throw new ArgumentException("Cliente inválido para inativação.");

                    await _clienteDAO.InativarAsync(cliente.Id, cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }

        }
    }
}
