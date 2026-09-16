using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRUD_Cliente2.Web.Models;

namespace CRUD_Cliente2.Web.Data
{
    public class ClienteDAO : IClienteDAO
    {
        private readonly AppDbContext _context;

        public ClienteDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente> ObterPorIdAsync(int id)
        {
            return await _context.Clientes
        .Include(c => c.EnderecoResidencial)
            .ThenInclude(e => e.Cidade)
                .ThenInclude(c => c.Estado)
                    .ThenInclude(e => e.Pais)
        .Include(c => c.EnderecoCobranca)
            .ThenInclude(e => e.Cidade)
                .ThenInclude(c => c.Estado)
                    .ThenInclude(e => e.Pais)
        .Include(c => c.Enderecos)
            .ThenInclude(e => e.Cidade)
                .ThenInclude(c => c.Estado)
                    .ThenInclude(e => e.Pais)
        .Include(c => c.Cartoes)
        .Include(c => c.Transacoes)
        .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            return await _context.Clientes
                .Include(c => c.EnderecoResidencial)
                .Include(c => c.EnderecoCobranca)
                .Include(c => c.Enderecos)
                .Include(c => c.Cartoes)
                .Include(c => c.Transacoes)
                .Where(c => c.Ativo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> BuscarPorFiltroAsync(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
                return await ObterTodosAsync();

            filtro = filtro.Trim().ToLower();

            return await _context.Clientes
                .Include(c => c.EnderecoResidencial)
                .Include(c => c.EnderecoCobranca)
                .Include(c => c.Enderecos)
                .Include(c => c.Cartoes)
                .Include(c => c.Transacoes)
                .Where(c => c.Ativo && (
                    c.Nome.ToLower().Contains(filtro) ||
                    c.CPF.ToLower().Contains(filtro) ||
                    c.Email.ToLower().Contains(filtro) ||
                    c.TelefoneNumero.ToLower().Contains(filtro)
                ))
                .ToListAsync();
        }

        public async Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken)
        {
            await _context.Clientes.AddAsync(cliente, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(int id, CancellationToken cancellationToken)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                cliente.Ativo = false;
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task AdicionarCartaoAsync(Cartao cartao)
        {
            if (cartao == null)
                throw new ArgumentNullException(nameof(cartao));

            await _context.Cartoes.AddAsync(cartao);
            await _context.SaveChangesAsync();
        }
        public async Task AdicionarEnderecoAsync(int clienteId, Endereco endereco)
        {
            endereco.ClienteId = clienteId;
            await _context.Enderecos.AddAsync(endereco);
            await _context.SaveChangesAsync();
        }
    }
}
