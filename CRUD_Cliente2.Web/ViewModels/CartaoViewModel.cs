using CRUD_Cliente2.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Cliente2.Web.ViewModels
{
    public class CartaoViewModel
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        [StringLength(19, MinimumLength = 13, ErrorMessage = "O número do cartão deve ter entre 13 e 19 dígitos.")]
        [RegularExpression(@"^\d{13,19}$", ErrorMessage = "O número do cartão deve conter apenas dígitos.")]
        public string NumeroCartao { get; set; }

        [Required]
        [MaxLength(100)]
        public string NomeImpresso { get; set; }

        [Required]
        [MaxLength(20)]
        public string Bandeira { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 3)]
        public string CodigoSeguranca { get; set; }

        public bool Preferencial { get; set; }
    }
}
