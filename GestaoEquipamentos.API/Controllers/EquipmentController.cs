using GestaoEquipamentos.Application.DTOs;
using GestaoEquipamentos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoEquipamentos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentService _service;

    public EquipmentController(IEquipmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? categoryId)
    {
        var result = categoryId.HasValue
            ? await _service.GetByCategoryIdAsync(categoryId.Value)
            : await _service.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEquipmentDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateEquipmentDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
