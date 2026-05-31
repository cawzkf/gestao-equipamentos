using GestaoEquipamentos.Application.DTOs;
using GestaoEquipamentos.Application.Interfaces;
using GestaoEquipamentos.Domain.Entities;

namespace GestaoEquipamentos.Application.Services;

public class EquipmentHistoryService : IEquipmentHistoryService
{
    private readonly IEquipmentHistoryRepository _repository;

    public EquipmentHistoryService(IEquipmentHistoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EquipmentHistoryDto>> GetAllAsync()
    {
        var records = await _repository.GetAllAsync();
        return records.Select(MapToDto);
    }

    public async Task<IEnumerable<EquipmentHistoryDto>> GetByEquipmentIdAsync(int equipmentId)
    {
        var records = await _repository.GetByEquipmentIdAsync(equipmentId);
        return records.Select(MapToDto);
    }

    public async Task<EquipmentHistoryDto?> GetByIdAsync(int id)
    {
        var record = await _repository.GetByIdAsync(id);
        return record is null ? null : MapToDto(record);
    }

    public async Task<EquipmentHistoryDto> CreateAsync(CreateEquipmentHistoryDto dto)
    {
        var record = new EquipmentHistory
        {
            EquipmentId = dto.EquipmentId,
            Action = dto.Action,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(record);
        return MapToDto(record);
    }

    public async Task DeleteAsync(int id)
    {
        var record = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Histórico não encontrado.");

        await _repository.DeleteAsync(record);
    }

    private static EquipmentHistoryDto MapToDto(EquipmentHistory h) =>
        new() { Id = h.Id, EquipmentId = h.EquipmentId, Action = h.Action, CreatedAt = h.CreatedAt };
}
