using Microsoft.EntityFrameworkCore;

namespace GestaoEquipamentos.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementação genérica de <see cref="IRepository{T}"/> sobre o EF Core.
///
/// Concentra o CRUD comum a todas as entidades em um único lugar. As propriedades
/// <see cref="Context"/> e <see cref="DbSet"/> são <c>protected</c> para que repositórios
/// específicos possam herdar desta classe e escrever consultas adicionais.
/// </summary>
/// <typeparam name="T">Tipo da entidade do domínio gerenciada por este repositório.</typeparam>
public class Repository<T> : IRepository<T> where T : class
{
    /// <summary>Contexto do EF Core, injetado pela DI (escopo por requisição).</summary>
    protected readonly AppDbContext Context;

    /// <summary>Conjunto (tabela) correspondente à entidade <typeparamref name="T"/>.</summary>
    protected readonly DbSet<T> DbSet;

    /// <summary>
    /// Cria o repositório genérico.
    /// :param context: contexto do EF Core fornecido pela DI.
    /// </summary>
    public Repository(AppDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await DbSet.AsNoTracking().ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await DbSet.FindAsync([id], cancellationToken);

    /// <inheritdoc />
    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }
}
