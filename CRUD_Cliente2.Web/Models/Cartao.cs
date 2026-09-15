using System.ComponentModel.DataAnnotations;

namespace CRUD_Cliente2.Web.Models
{
    public class Cartao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(4)]
        public string Ultimos4Digitos { get; set; }

        [Required]
        [MaxLength(100)]
        public string NomeImpresso { get; set; }

        [Required]
        [MaxLength(20)]
        public string Bandeira { get; set; }

        public bool Preferencial { get; set; }

        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }
    }
}
