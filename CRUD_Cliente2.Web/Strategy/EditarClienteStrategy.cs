using CRUD_Cliente2.Web.Data;
using CRUD_Cliente2.Web.Models;
using CRUD_Cliente2.Web.Validators;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Cliente2.Web.Strategy
{
    public class EditarClienteStrategy : IClienteStrategy
    {
        private readonly AppDbContext _context;
        private readonly IClienteDAO _clienteDAO;

        public EditarClienteStrategy(AppDbContext context, IClienteDAO clienteDAO
        )
        {
            _context = context;
            _clienteDAO = clienteDAO;
        }

        public async Task ExecutarAsync(Cliente clienteAtualizado, CancellationToken cancellationToken = default)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    if (clienteAtualizado == null || clienteAtualizado.Id == 0)
                        throw new ArgumentException("Cliente inválido para edição.");

                    var clienteExistente = await _context.Clientes
                        .FirstOrDefaultAsync(c => c.Id == clienteAtualizado.Id);

                    if (clienteExistente == null)
                        throw new InvalidOperationException("Cliente não encontrado.");

                    clienteExistente.Nome = clienteAtualizado.Nome;
                    clienteExistente.DataNascimento = clienteAtualizado.DataNascimento;
                    clienteExistente.CPF = ValidarCpf.Validar(clienteAtualizado.CPF);
                    clienteExistente.Email = ValidarEmail.Validar(clienteAtualizado.Email);
                    clienteExistente.Genero = clienteAtualizado.Genero;
                    clienteExistente.TelefoneTipo = clienteAtualizado.TelefoneTipo;
                    clienteExistente.TelefoneDDD = clienteAtualizado.TelefoneDDD;
                    clienteExistente.TelefoneNumero = clienteAtualizado.TelefoneNumero;

                    await _clienteDAO.AtualizarAsync(clienteExistente);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                }
            }

        }
    }
}
