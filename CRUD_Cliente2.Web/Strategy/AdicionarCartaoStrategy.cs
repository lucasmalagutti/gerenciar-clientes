using CRUD_Cliente2.Web.Data;
using CRUD_Cliente2.Web.Models;

namespace CRUD_Cliente2.Web.Strategy
{
    public class AdicionarCartaoStrategy : IAdicionarCartaoStrategy
    {
        private readonly IClienteDAO _clienteDAO;

        public AdicionarCartaoStrategy(IClienteDAO clienteDAO)
        {
            _clienteDAO = clienteDAO;
        }

        public async Task ExecutarAsync(Cartao cartao)
        {
            if (cartao == null)
                throw new ArgumentNullException(nameof(cartao));
            await _clienteDAO.AdicionarCartaoAsync(cartao);
        }
    }
}
