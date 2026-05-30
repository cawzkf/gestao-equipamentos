namespace GestaoEquipamentos.Infrastructure.Persistence.Repositories;

/// <summary>
/// Contrato genérico de repositório com as operações de persistência comuns (CRUD).
///
/// Fornece uma assinatura única reutilizável por qualquer entidade, evitando repetir
/// o mesmo CRUD em cada repositório. Repositórios específicos podem herdar a
/// implementação base (<see cref="Repository{T}"/>) e acrescentar apenas suas consultas próprias.
/// </summary>
/// <typeparam name="T">Tipo da entidade do domínio gerenciada por este repositório.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Retorna todas as entidades do tipo <typeparamref name="T"/>.
    /// :param cancellationToken: token de cancelamento da operação assíncrona.
    /// :return: lista somente-leitura com todas as entidades.
    /// </summary>
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca uma entidade pela chave primária.
    /// :param id: valor da chave primária.
    /// :param cancellationToken: token de cancelamento.
    /// :return: a entidade encontrada, ou <c>null</c> se não existir.
    /// </summary>
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adiciona uma nova entidade e persiste no banco.
    /// :param entity: entidade a ser inserida.
    /// :param cancellationToken: token de cancelamento.
    /// :return: a entidade já com a chave primária gerada pelo banco.
    /// </summary>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza uma entidade existente e persiste no banco.
    /// :param entity: entidade com os valores atualizados.
    /// :param cancellationToken: token de cancelamento.
    /// :return: tarefa concluída quando a atualização é salva.
    /// </summary>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove uma entidade do banco.
    /// :param entity: entidade a ser removida.
    /// :param cancellationToken: token de cancelamento.
    /// :return: tarefa concluída quando a remoção é salva.
    /// </summary>
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}
