using GlobalSoluction.DTOs.AlertaEstufa;

namespace GlobalSoluction.Interfaces;

public interface IAlertaEstufaService
{
    Task<List<AlertaEstufaRespostaDto>> ListarTodosAsync();
    Task<List<AlertaEstufaRespostaDto>> ListarPorEstufaAsync(int estufaConfigId);
    Task<bool> ResolverAsync(int id);
}