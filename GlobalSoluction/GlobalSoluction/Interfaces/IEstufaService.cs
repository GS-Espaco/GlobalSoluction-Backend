using GlobalSoluction.DTOs.Estufa;

namespace GlobalSoluction.Interfaces;

public interface IEstufaService
{
    Task<List<EstufaRespostaDto>> ListarTodasAsync();
    Task<EstufaRespostaDto?> BuscarPorIdAsync(int id);
    Task<EstufaRespostaDto> CriarAsync(CriarEstufaDto dto);
    Task<EstufaRespostaDto?> AtualizarAsync(int id, AtualizarEstufaDto dto);
    Task<bool> RemoverAsync(int id);
}