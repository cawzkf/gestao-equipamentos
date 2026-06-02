using GestaoEquipamentos.Application.Interfaces;
using GestaoEquipamentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoEquipamentos.Infrastructure.Persistence.Repositories;

public class EquipmentHistoryRepository : Repository<EquipmentHistory>, IEquipmentHistoryRepository
{
    public EquipmentHistoryRepository(AppDbContext context) : base(context) 
    {
    }   

    public async Task<IReadOnlyList<EquipmentHistory>> GetByEquipmentIdAsync(
        int equipmentId,
        CancellationToken cancellationToken = default
    ){
        return await DbSet
        .AsNoTracking()
        .Where(e => e.EquipmentId == equipmentId)
        .ToListAsync(cancellationToken);
    }
}

