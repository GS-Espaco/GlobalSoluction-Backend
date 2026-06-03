using GlobalSoluction.DTOs.AlertaEstufa;
using GlobalSoluction.Interfaces;
using GlobalSoluction.Models;

namespace GlobalSoluction.Services;

public class AlertaEstufaService : IAlertaEstufaService
{
    private readonly IAlertaEstufaRepository _alertaEstufaRepository;

    public AlertaEstufaService(IAlertaEstufaRepository alertaEstufaRepository)
    {
        _alertaEstufaRepository = alertaEstufaRepository;
    }

    public async Task<List<AlertaEstufaRespostaDto>> ListarTodosAsync()
    {
        var alertas = await _alertaEstufaRepository.ListarTodosAsync();

        return alertas.Select(alerta => MapearParaRespostaDto(alerta)).ToList();
    }

    public async Task<List<AlertaEstufaRespostaDto>> ListarPorEstufaAsync(int estufaConfigId)
    {
        var alertas = await _alertaEstufaRepository.ListarPorEstufaAsync(estufaConfigId);

        return alertas.Select(alerta => MapearParaRespostaDto(alerta)).ToList();
    }

    public async Task<bool> ResolverAsync(int id)
    {
        var alerta = await _alertaEstufaRepository.BuscarPorIdAsync(id);

        if (alerta == null)
        {
            return false;
        }

        alerta.Resolvido = true;
        alerta.DataResolucao = DateTime.Now;

        await _alertaEstufaRepository.AtualizarAsync(alerta);

        return true;
    }

    private static AlertaEstufaRespostaDto MapearParaRespostaDto(AlertaEstufa alerta)
    {
        return new AlertaEstufaRespostaDto
        {
            Id = alerta.Id,
            EstufaConfigId = alerta.EstufaConfigId,
            TipoSensor = alerta.TipoSensor,
            TipoAlerta = alerta.TipoAlerta,
            NivelCriticidade = alerta.NivelCriticidade,
            Mensagem = alerta.Mensagem,
            Recomendacao = alerta.Recomendacao,
            Resolvido = alerta.Resolvido,
            DataCriacao = alerta.DataCriacao,
            DataResolucao = alerta.DataResolucao
        };
    }
}