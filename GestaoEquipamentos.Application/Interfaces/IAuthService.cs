using GestaoEquipamentos.Application.DTOs;

namespace GestaoEquipamentos.Application.Interfaces;

public interface IAuthService
{
    Task<AuthTokenDto> RegisterAsync(RegisterDto dto);
    Task<AuthTokenDto> LoginAsync(LoginDto dto);
}
