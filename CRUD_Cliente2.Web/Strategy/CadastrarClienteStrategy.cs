using CRUD_Cliente2.Web.Data;
using CRUD_Cliente2.Web.Models;
using CRUD_Cliente2.Web.Validators;

namespace CRUD_Cliente2.Web.Strategy
{
    public class CadastrarClienteStrategy : IClienteStrategy
    {
        private readonly AppDbContext _context;
        private readonly IClienteDAO _clienteDAO;
        private readonly ICriptografarSenhaStrategy _criptografarSenhaStrategy;

        public CadastrarClienteStrategy(AppDbContext context, IClienteDAO clienteDAO, ICriptografarSenhaStrategy criptografarSenhaStrategy
        )
        {
            _context = context;
            _clienteDAO = clienteDAO;
            _criptografarSenhaStrategy = criptografarSenhaStrategy;

        }

        public async Task ExecutarAsync(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Senha) || cliente.Senha.Length < 8)
                throw new ArgumentException("Senha deve ter pelo menos 8 caracteres.");

            cliente.Senha = _criptografarSenhaStrategy.Criptografar(cliente.Senha);
            cliente.CPF = ValidarCpf.Validar(cliente.CPF);
            cliente.Email = ValidarEmail.Validar(cliente.Email);
            cliente.Ativo = true;
            cliente.Ranking = 0;

            if (cliente.EnderecoResidencial == null || cliente.EnderecoCobranca == null)
                throw new ArgumentException("Endereços obrigatórios.");

            await _context.Enderecos.AddAsync(cliente.EnderecoResidencial);
            await _context.Enderecos.AddAsync(cliente.EnderecoCobranca);
            await _context.SaveChangesAsync();

            cliente.EnderecoResidencialId = cliente.EnderecoResidencial.Id;
            cliente.EnderecoCobrancaId = cliente.EnderecoCobranca.Id;

            cliente.EnderecoResidencial.Cliente = cliente;
            cliente.EnderecoCobranca.Cliente = cliente;

            cliente.Enderecos.Add(cliente.EnderecoResidencial);
            cliente.Enderecos.Add(cliente.EnderecoCobranca);

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

    }
}
