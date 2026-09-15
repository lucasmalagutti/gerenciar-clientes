namespace CRUD_Cliente2.Web.ViewModels
{
    public class ClienteIndexViewModel
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public int Ranking { get; set; }
        public bool Ativo { get; set; }
    }
}
