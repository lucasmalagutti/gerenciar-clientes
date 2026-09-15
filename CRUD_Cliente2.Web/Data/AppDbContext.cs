using Microsoft.EntityFrameworkCore;
using CRUD_Cliente2.Web.Models;

namespace CRUD_Cliente2.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Endereco>()
    .HasOne(e => e.Cliente)
    .WithMany(c => c.Enderecos)
    .HasForeignKey(e => e.ClienteId)
    .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.EnderecoResidencial)
                 .WithMany()
                 .HasForeignKey(c => c.EnderecoResidencialId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.EnderecoCobranca)
                .WithMany()
                .HasForeignKey(c => c.EnderecoCobrancaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Endereco>()
                .Property(e => e.NomeIdentificador)
                .IsRequired(false);

            modelBuilder.Entity<Endereco>()
                .Property(e => e.Observacoes)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
