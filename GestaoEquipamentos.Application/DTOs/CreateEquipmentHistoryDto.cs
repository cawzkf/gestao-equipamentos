using System.ComponentModel.DataAnnotations;

namespace GestaoEquipamentos.Application.DTOs;

public class CreateEquipmentHistoryDto
{
    [Range(1, int.MaxValue, ErrorMessage = "EquipmentId inválido.")]
    public int EquipmentId { get; set; }

    [Required(ErrorMessage = "A ação é obrigatória.")]
    [MaxLength(200, ErrorMessage = "A ação deve ter no máximo 200 caracteres.")]
    public required string Action { get; set; }
}
