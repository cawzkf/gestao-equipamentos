using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GestaoEquipamentos.Infrastructure.Persistence;

/// <summary>
/// Fábrica usada pelo EF Core em tempo de design (ao rodar <c>dotnet ef migrations add</c>
/// ou <c>dotnet ef database update</c>). Permite gerar e aplicar migrations executando os
/// comandos diretamente neste projeto de Infraestrutura, sem depender da inicialização da API.
///
/// A connection string vem da variável de ambiente <c>CONNECTION_STRING</c>; quando ausente,
/// usa um padrão de desenvolvimento equivalente ao do docker-compose.
/// </summary>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    /// <summary>
    /// Cria uma instância do contexto para uso em tempo de design.
    /// :param args: argumentos repassados pelo dotnet-ef (não utilizados).
    /// :return: instância de <see cref="AppDbContext"/> configurada com o provider Npgsql.
    /// </summary>
    public AppDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            Environment.GetEnvironmentVariable("CONNECTION_STRING")
            ?? "Host=localhost;Port=5433;Database=gestao_equipamentos;Username=postgres;Password=postgres";

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new AppDbContext(options);
    }
}
