using GestaoEquipamentos.Domain.Entities;

namespace GestaoEquipamentos.Application.Interfaces;

public interface IEquipmentRepository
{
    Task<IReadOnlyList<Equipment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Equipment>> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<Equipment?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Equipment> AddAsync(Equipment entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Equipment entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Equipment entity, CancellationToken cancellationToken = default);
}
