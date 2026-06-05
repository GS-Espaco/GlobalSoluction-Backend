using GlobalSoluction.DTOs.Estufa;
using GlobalSoluction.Interfaces;
using GlobalSoluction.Models;

namespace GlobalSoluction.Services;

public class EstufaService : IEstufaService
{
    private readonly IEstufaRepository _estufaRepository;
    private readonly ILocalOrbitalRepository _localOrbitalRepository;

    public EstufaService(
        IEstufaRepository estufaRepository,
        ILocalOrbitalRepository localOrbitalRepository)
    {
        _estufaRepository = estufaRepository;
        _localOrbitalRepository = localOrbitalRepository;
    }

    public async Task<List<EstufaRespostaDto>> ListarTodasAsync()
    {
        var estufas = await _estufaRepository.ListarTodasAsync();

        return estufas.Select(estufa => MapearParaRespostaDto(estufa)).ToList();
    }

    public async Task<EstufaRespostaDto?> BuscarPorIdAsync(int id)
    {
        var estufa = await _estufaRepository.BuscarPorIdAsync(id);

        if (estufa == null)
        {
            return null;
        }

        return MapearParaRespostaDto(estufa);
    }

    public async Task<EstufaRespostaDto> CriarAsync(CriarEstufaDto dto)
    {
        var localOrbital = await _localOrbitalRepository.BuscarPorIdAsync(dto.LocalOrbitalId);

        if (localOrbital == null)
        {
            throw new KeyNotFoundException("LocalOrbital não encontrado.");
        }

        var estufa = new EstufaConfig
        {
            LocalOrbitalId = dto.LocalOrbitalId,
            Nome = dto.Nome,
            TipoPlantacao = dto.TipoPlantacao,
            TemperaturaIdealMin = dto.TemperaturaIdealMin,
            TemperaturaIdealMax = dto.TemperaturaIdealMax,
            UmidadeArIdealMin = dto.UmidadeArIdealMin,
            UmidadeArIdealMax = dto.UmidadeArIdealMax,
            UmidadeSoloIdealMin = dto.UmidadeSoloIdealMin,
            UmidadeSoloIdealMax = dto.UmidadeSoloIdealMax,
            LuminosidadeIdealMin = dto.LuminosidadeIdealMin,
            LuminosidadeIdealMax = dto.LuminosidadeIdealMax,
            Co2IdealMin = dto.Co2IdealMin,
            Co2IdealMax = dto.Co2IdealMax,
            Ativa = true,
            DataCriacao = DateTime.Now,
            DataAtualizacao = DateTime.Now
        };

        var estufaCriada = await _estufaRepository.CriarAsync(estufa);

        return MapearParaRespostaDto(estufaCriada);
    }

    public async Task<EstufaRespostaDto?> AtualizarAsync(int id, AtualizarEstufaDto dto)
    {
        var estufa = await _estufaRepository.BuscarPorIdAsync(id);

        if (estufa == null)
        {
            return null;
        }

        estufa.Nome = dto.Nome;
        estufa.TipoPlantacao = dto.TipoPlantacao;
        estufa.TemperaturaIdealMin = dto.TemperaturaIdealMin;
        estufa.TemperaturaIdealMax = dto.TemperaturaIdealMax;
        estufa.UmidadeArIdealMin = dto.UmidadeArIdealMin;
        estufa.UmidadeArIdealMax = dto.UmidadeArIdealMax;
        estufa.UmidadeSoloIdealMin = dto.UmidadeSoloIdealMin;
        estufa.UmidadeSoloIdealMax = dto.UmidadeSoloIdealMax;
        estufa.LuminosidadeIdealMin = dto.LuminosidadeIdealMin;
        estufa.LuminosidadeIdealMax = dto.LuminosidadeIdealMax;
        estufa.Co2IdealMin = dto.Co2IdealMin;
        estufa.Co2IdealMax = dto.Co2IdealMax;
        estufa.Ativa = dto.Ativa;
        estufa.DataAtualizacao = DateTime.Now;

        var estufaAtualizada = await _estufaRepository.AtualizarAsync(estufa);

        return MapearParaRespostaDto(estufaAtualizada);
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var estufa = await _estufaRepository.BuscarPorIdAsync(id);

        if (estufa == null)
        {
            return false;
        }

        await _estufaRepository.RemoverAsync(estufa);
        return true;
    }

    private static EstufaRespostaDto MapearParaRespostaDto(EstufaConfig estufa)
    {
        return new EstufaRespostaDto
        {
            Id = estufa.Id,
            Nome = estufa.Nome,
            TipoPlantacao = estufa.TipoPlantacao,
            Ativa = estufa.Ativa,
            DataCriacao = estufa.DataCriacao,
            LocalOrbitalId = estufa.LocalOrbitalId
        };
    }
}