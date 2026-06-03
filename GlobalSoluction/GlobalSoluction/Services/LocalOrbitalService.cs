using GlobalSoluction.DTOs.LocalOrbital;
using GlobalSoluction.Interfaces;
using GlobalSoluction.Models;
namespace GlobalSoluction.Services;

public class LocalOrbitalService : ILocalOrbitalService
{
    private readonly ILocalOrbitalRepository _localOrbitalRepository;

    public LocalOrbitalService(ILocalOrbitalRepository localOrbitalRepository)
    {
        _localOrbitalRepository = localOrbitalRepository;
    }

    public async Task<List<LocalOrbitalRespostaDto>> ListarTodosAsync()
    {
        var locais = await _localOrbitalRepository.ListarTodosAsync();

        return locais.Select(local => MapearParaRespostaDto(local)).ToList();
    }

    public async Task<LocalOrbitalRespostaDto?> BuscarPorIdAsync(int id)
    {
        var local = await _localOrbitalRepository.BuscarPorIdAsync(id);

        if (local == null)
        {
            return null;
        }

        return MapearParaRespostaDto(local);
    }

    public async Task<LocalOrbitalRespostaDto> CriarAsync(CriarLocalOrbitalDto dto)
    {
        var localOrbital = new LocalOrbital
        {
            Nome = dto.Nome,
            Planeta = dto.Planeta,
            Regiao = dto.Regiao,
            ProtecaoNatural = dto.ProtecaoNatural,
            IncidenciaSolar = dto.IncidenciaSolar,
            PossuiGeloSubterraneo = dto.PossuiGeloSubterraneo,
            NivelRiscoRadiacao = dto.NivelRiscoRadiacao,
            Observacoes = dto.Observacoes,
            DataAnalise = DateTime.Now
        };

        var localCriado = await _localOrbitalRepository.CriarAsync(localOrbital);

        return MapearParaRespostaDto(localCriado);
    }

    public async Task<LocalOrbitalRespostaDto?> AtualizarAsync(int id, AtualizarLocalOrbitalDto dto)
    {
        var local = await _localOrbitalRepository.BuscarPorIdAsync(id);

        if (local == null)
        {
            return null;
        }

        local.Nome = dto.Nome;
        local.Planeta = dto.Planeta;
        local.Regiao = dto.Regiao;
        local.ProtecaoNatural = dto.ProtecaoNatural;
        local.IncidenciaSolar = dto.IncidenciaSolar;
        local.PossuiGeloSubterraneo = dto.PossuiGeloSubterraneo;
        local.NivelRiscoRadiacao = dto.NivelRiscoRadiacao;
        local.Observacoes = dto.Observacoes;
        local.DataAnalise = DateTime.Now;

        var localAtualizado = await _localOrbitalRepository.AtualizarAsync(local);

        return MapearParaRespostaDto(localAtualizado);
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var local = await _localOrbitalRepository.BuscarPorIdAsync(id);

        if (local == null)
        {
            return false;
        }

        await _localOrbitalRepository.RemoverAsync(local);
        return true;
    }

    private static LocalOrbitalRespostaDto MapearParaRespostaDto(LocalOrbital local)
    {
        return new LocalOrbitalRespostaDto
        {
            Id = local.Id,
            Nome = local.Nome,
            Planeta = local.Planeta,
            Regiao = local.Regiao,
            ProtecaoNatural = local.ProtecaoNatural,
            IncidenciaSolar = local.IncidenciaSolar,
            PossuiGeloSubterraneo = local.PossuiGeloSubterraneo,
            NivelRiscoRadiacao = local.NivelRiscoRadiacao,
            Observacoes = local.Observacoes,
            DataAnalise = local.DataAnalise,
            QuantidadeEstufas = local.Estufas?.Count ?? 0
        };
    }
}