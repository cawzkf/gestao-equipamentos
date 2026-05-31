using GestaoEquipamentos.Application.Interfaces;
using GestaoEquipamentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoEquipamentos.Infrastructure.Persistence.Repositories;

public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
{
    public EquipmentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Equipment>> GetByCategoryIdAsync(
        int categoryId,
        CancellationToken cancellationToken = default

    ){
        return await DbSet
        .AsNoTracking()
        .Where(e => e.CategoryId == categoryId)
        .ToListAsync(cancellationToken);
    }
}
