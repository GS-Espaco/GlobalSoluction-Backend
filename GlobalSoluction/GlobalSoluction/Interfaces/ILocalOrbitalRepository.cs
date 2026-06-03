using GlobalSoluction.Models;

namespace GlobalSoluction.Interfaces;

public interface ILocalOrbitalRepository
{
    Task<List<LocalOrbital>> ListarTodosAsync();
    Task<LocalOrbital?> BuscarPorIdAsync(int id);
    Task<LocalOrbital> CriarAsync(LocalOrbital localOrbital);
    Task<LocalOrbital> AtualizarAsync(LocalOrbital localOrbital);
    Task RemoverAsync(LocalOrbital localOrbital);
}