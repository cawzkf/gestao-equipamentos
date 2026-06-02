using System.ComponentModel.DataAnnotations;

namespace GestaoEquipamentos.Application.DTOs;

public class UpdateSupplierDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "O e-mail de contato é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public required string ContactEmail { get; set; }
}
