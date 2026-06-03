using GestaoEquipamentos.Application.DTOs;
using GestaoEquipamentos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoEquipamentos.API.Controllers;

[Route("api/equipment-history")]
[ApiController]
[Authorize]
public class EquipmentHistoryController : ControllerBase
{
    private readonly IEquipmentHistoryService _service;

    public EquipmentHistoryController(IEquipmentHistoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("~/api/equipment/{equipmentId:int}/history")]
    public async Task<IActionResult> GetByEquipmentId(int equipmentId)
    {
        var result = await _service.GetByEquipmentIdAsync(equipmentId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEquipmentHistoryDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
