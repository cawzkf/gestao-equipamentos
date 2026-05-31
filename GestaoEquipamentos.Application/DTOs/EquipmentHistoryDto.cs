namespace GestaoEquipamentos.Application.DTOs;

public class EquipmentHistoryDto
{
    public int Id { get; set; }
    public int EquipmentId { get; set; }
    public required string Action { get; set; }
    public DateTime CreatedAt { get; set; }
}
