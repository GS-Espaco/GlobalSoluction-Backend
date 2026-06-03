using GlobalSoluction.Models;

namespace GlobalSoluction.Interfaces;

public interface IAlertaEstufaRepository
{
    Task<List<AlertaEstufa>> ListarTodosAsync();
    Task<List<AlertaEstufa>> ListarPorEstufaAsync(int estufaConfigId);
    Task<AlertaEstufa?> BuscarPorIdAsync(int id);
    Task<AlertaEstufa> CriarAsync(AlertaEstufa alerta);
    Task<AlertaEstufa> AtualizarAsync(AlertaEstufa alerta);
}