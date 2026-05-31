using GestaoEquipamentos.Application.DTOs;
using GestaoEquipamentos.Application.Interfaces;
using GestaoEquipamentos.Domain.Entities;

namespace GestaoEquipamentos.Application.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _repository;

    public SupplierService(ISupplierRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SupplierDto>> GetAllAsync()
    {
        var suppliers = await _repository.GetAllAsync();
        return suppliers.Select(MapToDto);
    }

    public async Task<SupplierDto?> GetByIdAsync(int id)
    {
        var supplier = await _repository.GetByIdAsync(id);
        return supplier is null ? null : MapToDto(supplier);
    }

    public async Task<SupplierDto> CreateAsync(CreateSupplierDto dto)
    {
        var supplier = new Supplier
        {
            Name = dto.Name,
            ContactEmail = dto.ContactEmail
        };

        await _repository.AddAsync(supplier);
        return MapToDto(supplier);
    }

    public async Task UpdateAsync(int id, UpdateSupplierDto dto)
    {
        var supplier = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Fornecedor não encontrado.");

        supplier.Name = dto.Name;
        supplier.ContactEmail = dto.ContactEmail;
        await _repository.UpdateAsync(supplier);
    }

    public async Task DeleteAsync(int id)
    {
        var supplier = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Fornecedor não encontrado.");

        await _repository.DeleteAsync(supplier);
    }

    private static SupplierDto MapToDto(Supplier supplier) =>
        new() { Id = supplier.Id, Name = supplier.Name, ContactEmail = supplier.ContactEmail };
}
