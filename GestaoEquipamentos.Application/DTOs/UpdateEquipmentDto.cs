using GestaoEquipamentos.Domain.Enums;

namespace GestaoEquipamentos.Application.DTOs;

public class UpdateEquipmentDto
{
    public required string Name { get; set; }
    public required string SerialNumber { get; set; }
    public required string Model { get; set; }
    public DateTime PurchaseDate { get; set; }
    public EquipmentStatusEnum Status { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }
}
