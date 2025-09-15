# Avaliação de Filmes (Console + EF Core)

Aplicação de console em C# para cadastrar avaliações de filmes com CRUD completo, persistência local usando Entity Framework Core e SQLite.

## 👥 Integrantes da Equipe

| **Nome**                   | **RM**   |
|-----------------------------|----------|
| Rodrigo Fernandes Serafim  | RM550816 |
| João Antonio Rihan         | RM99656  |
| Adriano Lopes              | RM98574  |
| Henrique de Brito          | RM98831  |
| Rodrigo Lima               | RM98326  |

## Visão geral
- Cadastro de avaliações com: Id, título do filme, estrelas (1 a 5) e descrição
- Operações: listar, adicionar, editar e remover
- Persistência com EF Core em arquivo SQLite (`movieratings.db`)
- Migrations aplicadas automaticamente ao iniciar

## Tecnologias e ferramentas
- **.NET**: 9.0 (Console App)
- **Entity Framework Core**: 9.0
  - Provider: `Microsoft.EntityFrameworkCore.Sqlite`
  - Design-time: `Microsoft.EntityFrameworkCore.Design`
- **SQLite**: banco de dados local em arquivo
- **dotnet-ef**: ferramenta de linha de comando para migrations

## Como executar
1. Requisitos: .NET SDK 9 instalado
2. No diretório do projeto, execute:
```bash
# Restaurar e compilar
dotnet build .\MovieRatings.Console\MovieRatings.Console.csproj

# Rodar a aplicação de console
dotnet run --project .\MovieRatings.Console\MovieRatings.Console.csproj
```
Ao iniciar, o app aplica as migrations pendentes e mostra o menu no console.

## Estrutura de pastas (resumo)
```
MovieRatings.Console/
  Data/
    Db.cs                      # Contexto EF Core (ContextoDados)
    DbFactory.cs               # Fábrica design-time para dotnet-ef
    Migrations/                # Migrations geradas pelo EF
  Models/
    Review.cs                  # Entidade Avaliacao
  Repositories/
    ReviewRepository.cs        # AvaliacaoRepositorio (CRUD)
  Program.cs                   # Menu do console e fluxo da aplicação
movieratings.db                 # Banco de dados SQLite (gerado em runtime)
```

## Fluxo do console (CRUD)
1) Listar avaliações
2) Adicionar avaliação
3) Editar avaliação
4) Remover avaliação
0) Sair

Validações básicas:
- Estrelas devem estar entre 1 e 5
- Título é obrigatório (até 200 caracteres)
- Descrição é opcional (até 1000 caracteres)
---
