using System;
using System.ComponentModel.DataAnnotations;

namespace MovieRatings.ConsoleApp.Models
{
    // Representa uma avaliação de filme feita pelo usuário
    public class Avaliacao
    {
        // Identificador único no banco de dados
        public int Id { get; set; }

        // Nome/Título do filme
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        // Quantidade de estrelas (somente de 1 a 5)
        [Range(1, 5)]
        public int Stars { get; set; }

        // Comentário livre da avaliação (opcional)
        [MaxLength(1000)]
        public string? Description { get; set; }

        // Momento em que a avaliação foi criada (UTC)
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}


