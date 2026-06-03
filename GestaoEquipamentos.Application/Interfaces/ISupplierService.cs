using GestaoEquipamentos.Application.DTOs;

namespace GestaoEquipamentos.Application.Interfaces;

public interface ISupplierService
{
    Task<IEnumerable<SupplierDto>> GetAllAsync();
    Task<SupplierDto> GetByIdAsync(int id);
    Task<SupplierDto> CreateAsync(CreateSupplierDto dto);
    Task UpdateAsync(int id, UpdateSupplierDto dto);
    Task DeleteAsync(int id);
}
