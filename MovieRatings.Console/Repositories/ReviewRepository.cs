using Microsoft.EntityFrameworkCore;
using MovieRatings.ConsoleApp.Data;
using MovieRatings.ConsoleApp.Models;

namespace MovieRatings.ConsoleApp.Repositories
{
    // Repositório com operações de CRUD para a entidade Avaliacao.
    // Centraliza o acesso ao banco para manter o Program.cs mais limpo.
    public class AvaliacaoRepositorio
    {
        private readonly ContextoDados _dbContext;

        public AvaliacaoRepositorio(ContextoDados dbContext)
        {
            _dbContext = dbContext;
        }

        // Cria uma nova avaliação
        public async Task<Avaliacao> CriarAsync(Avaliacao avaliacao)
        {
            _dbContext.Avaliacoes.Add(avaliacao);
            await _dbContext.SaveChangesAsync();
            return avaliacao;
        }

        // Busca uma avaliação pelo Id
        public async Task<Avaliacao?> ObterPorIdAsync(int id)
        {
            return await _dbContext.Avaliacoes.FindAsync(id);
        }

        // Lista todas as avaliações, da mais recente para a mais antiga
        public async Task<List<Avaliacao>> ObterTodosAsync()
        {
            return await _dbContext.Avaliacoes
                .OrderByDescending(r => r.CreatedAtUtc)
                .ToListAsync();
        }

        // Atualiza os dados de uma avaliação existente
        public async Task<bool> AtualizarAsync(Avaliacao avaliacao)
        {
            var exists = await _dbContext.Avaliacoes.AnyAsync(r => r.Id == avaliacao.Id);
            if (!exists) return false;

            _dbContext.Avaliacoes.Update(avaliacao);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // Remove uma avaliação pelo Id
        public async Task<bool> RemoverAsync(int id)
        {
            var entity = await _dbContext.Avaliacoes.FindAsync(id);
            if (entity == null) return false;
            _dbContext.Avaliacoes.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}


