using GlobalSoluction.DTOs.Auth;

namespace GlobalSoluction.Interfaces;

public interface IAuthService
{
    Task<AuthRespostaDto?> RegistrarAsync(RegistrarUsuarioDto dto);
    Task<AuthRespostaDto?> LoginAsync(LoginDto dto);
}