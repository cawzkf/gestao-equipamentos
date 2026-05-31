using GestaoEquipamentos.Domain.Entities;

namespace GestaoEquipamentos.Application.Interfaces;

public interface IEquipmentHistoryRepository
{
    Task<IReadOnlyList<EquipmentHistory>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EquipmentHistory>> GetByEquipmentIdAsync(int equipmentId, CancellationToken cancellationToken = default);
    Task<EquipmentHistory?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<EquipmentHistory> AddAsync(EquipmentHistory entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(EquipmentHistory entity, CancellationToken cancellationToken = default);
}
