using GestaoEquipamentos.Application.DTOs;
using GestaoEquipamentos.Application.Interfaces;
using GestaoEquipamentos.Domain.Entities;
using GestaoEquipamentos.Domain.Enums;

namespace GestaoEquipamentos.Application.Services;

public class EquipmentService : IEquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;

    public EquipmentService(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    public async Task<IEnumerable<EquipmentDto>> GetAllAsync()
    {
        var equipments = await _equipmentRepository.GetAllAsync();
        return equipments.Select(MapToDto);
    }

    public async Task<EquipmentDto?> GetByIdAsync(int id)
    {
        var equipment = await _equipmentRepository.GetByIdAsync(id);
        return equipment is null ? null : MapToDto(equipment);
    }

    public async Task<EquipmentDto> CreateAsync(CreateEquipmentDto dto)
    {
        var equipment = new Equipment
        {
            Name = dto.Name,
            SerialNumber = dto.SerialNumber,
            Model = dto.Model,
            PurchaseDate = dto.PurchaseDate,
            Status = EquipmentStatusEnum.Available,
            CategoryId = dto.CategoryId,
            SupplierId = dto.SupplierId
        };

        await _equipmentRepository.AddAsync(equipment);
        return MapToDto(equipment);
    }

    public async Task UpdateAsync(int id, UpdateEquipmentDto dto)
    {
        var equipment = await _equipmentRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Equipamento não encontrado.");

        equipment.Name = dto.Name;
        equipment.SerialNumber = dto.SerialNumber;
        equipment.Model = dto.Model;
        equipment.PurchaseDate = dto.PurchaseDate;
        equipment.Status = dto.Status;
        equipment.CategoryId = dto.CategoryId;
        equipment.SupplierId = dto.SupplierId;

        await _equipmentRepository.UpdateAsync(equipment);
    }

    public async Task DeleteAsync(int id)
    {
        var equipment = await _equipmentRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Equipamento não encontrado.");

        await _equipmentRepository.DeleteAsync(equipment);
    }

    private static EquipmentDto MapToDto(Equipment equipment)
    {
        return new EquipmentDto
        {
            Id = equipment.Id,
            Name = equipment.Name,
            SerialNumber = equipment.SerialNumber,
            Model = equipment.Model,
            PurchaseDate = equipment.PurchaseDate,
            Status = equipment.Status.ToString(),
            CategoryId = equipment.CategoryId,
            SupplierId = equipment.SupplierId
        };
    }
}
