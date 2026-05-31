namespace GestaoEquipamentos.Application.DTOs;

public class EquipmentDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string SerialNumber { get; set; }
    public required string Model { get; set; }
    public DateTime PurchaseDate { get; set; }
    public required string Status { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }
}
