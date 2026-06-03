using GlobalSoluction.DTOs.LeituraSensor;

namespace GlobalSoluction.Interfaces;

public interface ILeituraSensorService
{
    Task<List<LeituraSensorRespostaDto>> ListarTodasAsync();
    Task<List<LeituraSensorRespostaDto>> ListarPorEstufaAsync(int estufaConfigId);
    Task<LeituraSensorRespostaDto?> CriarAsync(CriarLeituraSensorDto dto);
}