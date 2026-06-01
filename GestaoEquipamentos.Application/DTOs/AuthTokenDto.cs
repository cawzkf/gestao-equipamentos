namespace GestaoEquipamentos.Application.DTOs;

public class AuthTokenDto
{
    public required string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}
