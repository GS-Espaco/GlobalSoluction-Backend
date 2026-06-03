using GlobalSoluction.Models;

namespace GlobalSoluction.Interfaces;

public interface IEstufaRepository
{
    Task<List<EstufaConfig>> ListarTodasAsync();
    Task<EstufaConfig?> BuscarPorIdAsync(int id);
    Task<EstufaConfig> CriarAsync(EstufaConfig estufa);
    Task<EstufaConfig> AtualizarAsync(EstufaConfig estufa);
    Task RemoverAsync(EstufaConfig estufa);
}