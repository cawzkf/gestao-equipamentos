using GestaoEquipamentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GestaoEquipamentos.Infrastructure.Persistence;

/// <summary>
/// Contexto do Entity Framework Core — a ponte entre as entidades do domínio e o PostgreSQL.
///
/// É aqui que serão declarados os <c>DbSet</c> (um por entidade do domínio) e onde os
/// mapeamentos são aplicados em <see cref="OnModelCreating"/>. Os mapeamentos não ficam
/// soltos neste arquivo: cada entidade ganha sua própria classe que implementa
/// <c>IEntityTypeConfiguration&lt;T&gt;</c> (na pasta Persistence/Configurations) e todas são
/// aplicadas automaticamente pelo <c>ApplyConfigurationsFromAssembly</c>.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Cria o contexto. As opções (provider Npgsql e connection string) são fornecidas
    /// pela injeção de dependência, configuradas em <c>DependencyInjection.AddInfrastructure</c>.
    /// :param options: opções de configuração do contexto fornecidas pela DI.
    /// </summary>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Equipment> Equipments => Set<Equipment>();

    /// <summary>
    /// Configura o modelo do banco aplicando todas as classes de mapeamento
    /// (<c>IEntityTypeConfiguration&lt;T&gt;</c>) encontradas neste assembly de Infraestrutura.
    /// :param modelBuilder: construtor do modelo fornecido pelo EF Core.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
