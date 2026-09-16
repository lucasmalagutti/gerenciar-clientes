using CRUD_Cliente2.Web.Models;

namespace CRUD_Cliente2.Web.Data
{
    public interface IClienteDAO
    {
        Task<Cliente> ObterPorIdAsync(int id);
        Task<IEnumerable<Cliente>> ObterTodosAsync();
        Task<IEnumerable<Cliente>> BuscarPorFiltroAsync(string filtro);
        Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken);
        Task AtualizarAsync(Cliente cliente);
        Task InativarAsync(int id, CancellationToken cancellationToken);
        Task AdicionarCartaoAsync(Cartao cartao);
        Task AdicionarEnderecoAsync(int clienteId, Endereco endereco);
    }
}
