using GestaoEquipamentos.Application.DTOs;

namespace GestaoEquipamentos.Application.Interfaces;

public interface IEquipmentService
{
    Task<IEnumerable<EquipmentDto>> GetAllAsync();
    Task<EquipmentDto?> GetByIdAsync(int id);
    Task<EquipmentDto> CreateAsync(CreateEquipmentDto dto);
    Task UpdateAsync(int id, UpdateEquipmentDto dto);
    Task DeleteAsync(int id);
}
