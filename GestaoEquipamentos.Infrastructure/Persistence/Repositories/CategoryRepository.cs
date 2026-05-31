using GestaoEquipamentos.Application.Interfaces;
using GestaoEquipamentos.Domain.Entities;

namespace GestaoEquipamentos.Infrastructure.Persistence.Repositories;


public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) 
    {
    }   

}