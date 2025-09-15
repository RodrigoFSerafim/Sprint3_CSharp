using Microsoft.EntityFrameworkCore;
using MovieRatings.ConsoleApp.Models;

namespace MovieRatings.ConsoleApp.Data
{
    // Contexto de dados do Entity Framework Core.
    // Ele representa a conexão com o banco e o mapeamento das entidades.
    public class ContextoDados : DbContext
    {
        // Conjunto de Avaliações (tabela) no banco de dados
        public DbSet<Avaliacao> Avaliacoes => Set<Avaliacao>();

        // Construtor recebendo as opções (ex.: provedor SQLite)
        public ContextoDados(DbContextOptions<ContextoDados> options) : base(options)
        {
        }

        // Configurações de mapeamento das entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avaliacao>(entity =>
            {
                // Mantemos o nome da tabela original para não quebrar o banco existente
                entity.ToTable("MovieReviews");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Title).IsRequired().HasMaxLength(200);
                entity.Property(r => r.Stars).IsRequired();
                entity.Property(r => r.Description).HasMaxLength(1000);
                entity.Property(r => r.CreatedAtUtc).IsRequired();
            });
        }
    }
}


