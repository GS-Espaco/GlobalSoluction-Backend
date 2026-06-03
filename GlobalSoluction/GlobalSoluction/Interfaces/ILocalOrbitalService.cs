using GlobalSoluction.DTOs.LocalOrbital;

namespace GlobalSoluction.Interfaces;

public interface ILocalOrbitalService
{
    Task<List<LocalOrbitalRespostaDto>> ListarTodosAsync();
    Task<LocalOrbitalRespostaDto?> BuscarPorIdAsync(int id);
    Task<LocalOrbitalRespostaDto> CriarAsync(CriarLocalOrbitalDto dto);
    Task<LocalOrbitalRespostaDto?> AtualizarAsync(int id, AtualizarLocalOrbitalDto dto);
    Task<bool> RemoverAsync(int id);
}