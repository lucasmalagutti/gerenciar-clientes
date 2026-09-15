using CRUD_Cliente2.Web.Data;

namespace CRUD_Cliente2.Web.Strategy
{
    public class AlterarSenhaStrategy : IClienteSenhaStrategy
    {
        private readonly AppDbContext _context;
        private readonly ICriptografarSenhaStrategy _criptografarSenhaStrategy;

        public AlterarSenhaStrategy(AppDbContext context, ICriptografarSenhaStrategy criptografarSenhaStrategy)
        {
            _context = context;
            _criptografarSenhaStrategy = criptografarSenhaStrategy;
        }

        public async Task AlterarSenhaAsync(int clienteId, string novaSenha)
        {
            if (string.IsNullOrWhiteSpace(novaSenha) || novaSenha.Length < 8)
                throw new ArgumentException("Senha deve ter pelo menos 8 caracteres.");

            var cliente = await _context.Clientes.FindAsync(clienteId);
            if (cliente == null)
                throw new ArgumentException("Cliente não encontrado.");

            cliente.Senha = _criptografarSenhaStrategy.Criptografar(novaSenha);

            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
