using System.ComponentModel.DataAnnotations;
using CRUD_Cliente2.Web.Validators;

namespace CRUD_Cliente2.Web.ViewModels
{
    public class ClienteFormViewModel
    {
        [Required(ErrorMessage = "Informe o nome.")]
        [MaxLength(100)]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Informe a data de nascimento.")]
        [DataNascimentoValida(idadeMinima: 18, ErrorMessage = "Você deve ser maior de idade.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Informe o cpf.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF inválido")]
        public required string CPF { get; set; }

        [Required(ErrorMessage = "Informe o email.")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Informe o gênero.")]
        public required string Genero { get; set; }

        [Required(ErrorMessage = "Informe o tipo de telefone.")]
        public required string TelefoneTipo { get; set; }

        [Required(ErrorMessage = "Informe o ddd do telefone.")]
        [RegularExpression(@"^\d{2}$")]
        public required string TelefoneDDD { get; set; }

        [Required(ErrorMessage = "Informe o numero de telefone.")]
        [RegularExpression(@"^\d{8,9}$")]
        public required string TelefoneNumero { get; set; }
    }
}
