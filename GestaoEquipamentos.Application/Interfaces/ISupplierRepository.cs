using GestaoEquipamentos.Domain.Entities;

namespace GestaoEquipamentos.Application.Interfaces;

public interface ISupplierRepository
{
    Task<IReadOnlyList<Supplier>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Supplier?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Supplier> AddAsync(Supplier entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Supplier entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Supplier entity, CancellationToken cancellationToken = default);
}
