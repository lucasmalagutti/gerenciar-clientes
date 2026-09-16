using CRUD_Cliente2.Web.Data;
using CRUD_Cliente2.Web.Models;

namespace CRUD_Cliente2.Web.Strategy
{
    public class AdicionarEnderecoStrategy : IAdicionarEnderecoStrategy
    {
        private readonly IClienteDAO _clienteDAO;
        private readonly AppDbContext _context;

        public AdicionarEnderecoStrategy(IClienteDAO clienteDAO, AppDbContext context)
        {
            _clienteDAO = clienteDAO;
            _context = context;
        }

        public async Task ExecutarAsync(int clienteId, Endereco endereco)
        {
            if (clienteId == 0)
                throw new InvalidOperationException("Cliente não encontrado.");

            await _clienteDAO.AdicionarEnderecoAsync(clienteId, endereco);
        }
    }
}
