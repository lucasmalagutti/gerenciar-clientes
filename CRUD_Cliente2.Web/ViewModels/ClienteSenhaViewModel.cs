using System.ComponentModel.DataAnnotations;

namespace CRUD_Cliente2.Web.ViewModels
{
    public class ClienteSenhaViewModel
    {
        [Required]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Informe nova senha.")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirme nova senha.")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não coincidem")]
        [DataType(DataType.Password)]
        public string ConfirmarSenha { get; set; }
    }
}
