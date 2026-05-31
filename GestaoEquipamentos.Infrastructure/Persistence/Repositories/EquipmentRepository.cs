using GestaoEquipamentos.Application.Interfaces;
using GestaoEquipamentos.Domain.Entities;

namespace GestaoEquipamentos.Infrastructure.Persistence.Repositories;

public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
{
    public EquipmentRepository(AppDbContext context) : base(context)
    {
    }
}
