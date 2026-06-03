using GestaoEquipamentos.Application.DTOs;

namespace GestaoEquipamentos.Application.Interfaces;

public interface IEquipmentHistoryService
{
    Task<IEnumerable<EquipmentHistoryDto>> GetAllAsync();
    Task<IEnumerable<EquipmentHistoryDto>> GetByEquipmentIdAsync(int equipmentId);
    Task<EquipmentHistoryDto> GetByIdAsync(int id);
    Task<EquipmentHistoryDto> CreateAsync(CreateEquipmentHistoryDto dto);
    Task DeleteAsync(int id);
}
