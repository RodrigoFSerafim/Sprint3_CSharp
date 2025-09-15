using Microsoft.EntityFrameworkCore;
using MovieRatings.ConsoleApp.Data;
using MovieRatings.ConsoleApp.Models;
using MovieRatings.ConsoleApp.Repositories;
 

// Configura o EF Core para usar SQLite e abre a conexão com o banco
var options = new DbContextOptionsBuilder<ContextoDados>()
    .UseSqlite("Data Source=movieratings.db")
    .Options;

// Garante que o banco/migrations estejam aplicados ao iniciar o app
await using var db = new ContextoDados(options);
await db.Database.MigrateAsync();

// Repositório para manipular as avaliações
var repo = new AvaliacaoRepositorio(db);

// Loop principal do menu do console
bool exit = false;
while (!exit)
{
    Console.WriteLine();
    Console.WriteLine("==== Avaliação de Filmes ====");
    Console.WriteLine("1) Listar avaliações");
    Console.WriteLine("2) Adicionar avaliação");
    Console.WriteLine("3) Editar avaliação");
    Console.WriteLine("4) Remover avaliação");
    
    Console.WriteLine("0) Sair");
    Console.Write("Escolha: ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await ListarAsync(repo);
            break;
        case "2":
            await AdicionarAsync(repo);
            break;
        case "3":
            await EditarAsync(repo);
            break;
        case "4":
            await RemoverAsync(repo);
            break;
        case "0":
            exit = true;
            break;
        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}

// Mostra todas as avaliações cadastradas
static async Task ListarAsync(AvaliacaoRepositorio repo)
{
    var items = await repo.ObterTodosAsync();
    if (items.Count == 0)
    {
        Console.WriteLine("Nenhuma avaliação encontrada.");
        return;
    }
    foreach (var r in items)
    {
        Console.WriteLine($"#{r.Id} - {r.Title} | {r.Stars} estrelas | {r.CreatedAtUtc:u}");
        if (!string.IsNullOrWhiteSpace(r.Description))
        {
            Console.WriteLine($"  {r.Description}");
        }
    }
}

// Solicita os dados ao usuário e cria uma nova avaliação
static async Task AdicionarAsync(AvaliacaoRepositorio repo)
{
    Console.Write("Nome do filme: ");
    var title = Console.ReadLine()?.Trim() ?? string.Empty;

    int stars = ReadStars();

    Console.Write("Descrição (opcional): ");
    var desc = Console.ReadLine();

    var avaliacao = new Avaliacao { Title = title, Stars = stars, Description = desc };
    await repo.CriarAsync(avaliacao);
    Console.WriteLine("Avaliação adicionada com sucesso.");
}

// Edita os dados de uma avaliação existente
static async Task EditarAsync(AvaliacaoRepositorio repo)
{
    Console.Write("Id da avaliação para editar: ");
    if (!int.TryParse(Console.ReadLine(), out var id))
    {
        Console.WriteLine("Id inválido.");
        return;
    }
    var existing = await repo.ObterPorIdAsync(id);
    if (existing == null)
    {
        Console.WriteLine("Avaliação não encontrada.");
        return;
    }

    Console.Write($"Nome do filme ({existing.Title}): ");
    var title = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(title)) existing.Title = title.Trim();

    Console.Write($"Estrelas ({existing.Stars}): ");
    var starsInput = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(starsInput) && int.TryParse(starsInput, out var newStars))
    {
        if (newStars < 1 || newStars > 5)
        {
            Console.WriteLine("Valor de estrelas deve ser entre 1 e 5. Mantendo o valor antigo.");
        }
        else
        {
            existing.Stars = newStars;
        }
    }

    Console.Write($"Descrição ({existing.Description}): ");
    var desc = Console.ReadLine();
    if (desc != null) existing.Description = desc;

    var ok = await repo.AtualizarAsync(existing);
    Console.WriteLine(ok ? "Atualizado." : "Falha ao atualizar.");
}

// Remove uma avaliação a partir do Id
static async Task RemoverAsync(AvaliacaoRepositorio repo)
{
    Console.Write("Id da avaliação para remover: ");
    if (!int.TryParse(Console.ReadLine(), out var id))
    {
        Console.WriteLine("Id inválido.");
        return;
    }
    var ok = await repo.RemoverAsync(id);
    Console.WriteLine(ok ? "Removido." : "Avaliação não encontrada.");
}

// Lê e valida a quantidade de estrelas (1 a 5) digitada pelo usuário
static int ReadStars()
{
    while (true)
    {
        Console.Write("Estrelas (1-5): ");
        var starsStr = Console.ReadLine();
        if (int.TryParse(starsStr, out var stars) && stars >= 1 && stars <= 5)
        {
            return stars;
        }
        Console.WriteLine("Valor inválido. Tente novamente.");
    }
}
