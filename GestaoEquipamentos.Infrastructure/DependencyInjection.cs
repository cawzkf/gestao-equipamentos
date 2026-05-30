using GestaoEquipamentos.Infrastructure.Persistence;
using GestaoEquipamentos.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoEquipamentos.Infrastructure;

/// <summary>
/// Ponto único de registro dos serviços da camada de Infraestrutura no contêiner de DI:
/// o <see cref="AppDbContext"/> (com o provider Npgsql/PostgreSQL) e o repositório genérico.
///
/// A camada API só precisa chamar <c>builder.Services.AddInfrastructure(builder.Configuration)</c>,
/// sem conhecer os detalhes de configuração do banco.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registra o DbContext (PostgreSQL via Npgsql) e o repositório genérico.
    /// :param services: coleção de serviços do ASP.NET Core.
    /// :param configuration: configuração da aplicação; lê a connection string "DefaultConnection".
    /// :return: a própria coleção de serviços, permitindo o encadeamento de chamadas.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' não encontrada. " +
                "Defina-a em appsettings.json ou em uma variável de ambiente.");

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
