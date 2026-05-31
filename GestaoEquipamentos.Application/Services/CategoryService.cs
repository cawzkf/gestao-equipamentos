using GestaoEquipamentos.Application.DTOs;
using GestaoEquipamentos.Application.Interfaces;
using GestaoEquipamentos.Domain.Entities;

namespace GestaoEquipamentos.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(MapToDto);
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        return category is null ? null : MapToDto(category);
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category { Name = dto.Name };
        await _repository.AddAsync(category);
        return MapToDto(category);
    }

    public async Task UpdateAsync(int id, UpdateCategoryDto dto)
    {
        var category = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Categoria não encontrada.");

        category.Name = dto.Name;
        await _repository.UpdateAsync(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Categoria não encontrada.");

        await _repository.DeleteAsync(category);
    }

    private static CategoryDto MapToDto(Category category) =>
        new() { Id = category.Id, Name = category.Name };
}
