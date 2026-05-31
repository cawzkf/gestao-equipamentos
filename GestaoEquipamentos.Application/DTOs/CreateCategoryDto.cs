using System.ComponentModel.DataAnnotations;

namespace GestaoEquipamentos.Application.DTOs;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public required string Name { get; set; }
}
