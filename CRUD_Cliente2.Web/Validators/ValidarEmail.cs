using System.Net.Mail;
namespace CRUD_Cliente2.Web.Validators
{
    public static class ValidarEmail
    {
        public static string Validar(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("O e-mail não pode ser vazio.", nameof(email));
            }

            try
            {
                var mailAddress = new MailAddress(email);

                return mailAddress.Address;
            }
            catch (FormatException)
            {
                throw new ArgumentException("E-mail inválido.", nameof(email));
            }
        }
    }
}
