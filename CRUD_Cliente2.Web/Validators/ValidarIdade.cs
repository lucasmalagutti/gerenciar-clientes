using System.ComponentModel.DataAnnotations;

namespace CRUD_Cliente2.Web.Validators
{
    public class DataNascimentoValida : ValidationAttribute
    {
        private readonly int _idadeMinima;
        public DataNascimentoValida(int idadeMinima = 0)
        {
            _idadeMinima = idadeMinima;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime dataNascimento)
            {
                return new ValidationResult("Data de nascimento inválida");
            }
            if (dataNascimento > DateTime.Today)
            {
                return new ValidationResult("A data de nascimento não pode ser no futuro");
            }

            var idade = DateTime.Today.Year - dataNascimento.Year;
            if (dataNascimento.Date > DateTime.Today.AddYears(-idade)) idade--;

            if (idade < _idadeMinima)
            {
                return new ValidationResult($"A idade mínima permitida é de {_idadeMinima} anos.");
            }

            return ValidationResult.Success;
        }
    }
}