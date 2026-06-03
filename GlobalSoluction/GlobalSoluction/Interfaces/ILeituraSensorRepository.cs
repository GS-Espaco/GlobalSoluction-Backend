using GlobalSoluction.Models;

namespace GlobalSoluction.Interfaces;

public interface ILeituraSensorRepository
{
    Task<List<LeituraSensor>> ListarTodasAsync();
    Task<List<LeituraSensor>> ListarPorEstufaAsync(int estufaConfigId);
    Task<LeituraSensor> CriarAsync(LeituraSensor leitura);
}