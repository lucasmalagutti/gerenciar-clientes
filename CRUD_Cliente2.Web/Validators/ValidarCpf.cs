
using DocumentValidator;
namespace CRUD_Cliente2.Web.Validators
{
    public static class ValidarCpf
    {
        public static string Validar(string cpf)
        {
            if (!CpfValidation.Validate(cpf))
            {
                throw new ArgumentException("Cpf inválido.", nameof(cpf));
            }
            return cpf;
        }
    }
}
