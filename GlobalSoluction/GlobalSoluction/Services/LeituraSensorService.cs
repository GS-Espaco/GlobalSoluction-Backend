using GlobalSoluction.DTOs.LeituraSensor;
using GlobalSoluction.Enums;
using GlobalSoluction.Interfaces;
using GlobalSoluction.Models;

namespace GlobalSoluction.Services;

public class LeituraSensorService : ILeituraSensorService
{
    private readonly ILeituraSensorRepository _leituraSensorRepository;
    private readonly IEstufaRepository _estufaRepository;
    private readonly IAlertaEstufaRepository _alertaEstufaRepository;

    public LeituraSensorService(
        ILeituraSensorRepository leituraSensorRepository,
        IEstufaRepository estufaRepository,
        IAlertaEstufaRepository alertaEstufaRepository)
    {
        _leituraSensorRepository = leituraSensorRepository;
        _estufaRepository = estufaRepository;
        _alertaEstufaRepository = alertaEstufaRepository;
    }

    public async Task<List<LeituraSensorRespostaDto>> ListarTodasAsync()
    {
        var leituras = await _leituraSensorRepository.ListarTodasAsync();

        return leituras.Select(leitura => MapearParaRespostaDto(leitura)).ToList();
    }

    public async Task<List<LeituraSensorRespostaDto>> ListarPorEstufaAsync(int estufaConfigId)
    {
        var leituras = await _leituraSensorRepository.ListarPorEstufaAsync(estufaConfigId);

        return leituras.Select(leitura => MapearParaRespostaDto(leitura)).ToList();
    }

    public async Task<LeituraSensorRespostaDto?> CriarAsync(CriarLeituraSensorDto dto)
    {
        var estufa = await _estufaRepository.BuscarPorIdAsync(dto.EstufaConfigId);

        if (estufa == null)
        {
            return null;
        }

        var leitura = new LeituraSensor
        {
            EstufaConfigId = dto.EstufaConfigId,
            TipoSensor = dto.TipoSensor,
            Valor = dto.Valor,
            DataLeitura = DateTime.Now
        };

        var leituraCriada = await _leituraSensorRepository.CriarAsync(leitura);

        var alerta = GerarAlertaSeNecessario(estufa, leituraCriada);

        if (alerta != null)
        {
            await _alertaEstufaRepository.CriarAsync(alerta);
        }

        return MapearParaRespostaDto(leituraCriada);
    }

    private static AlertaEstufa? GerarAlertaSeNecessario(EstufaConfig estufa, LeituraSensor leitura)
    {
        decimal minimo;
        decimal maximo;

        switch (leitura.TipoSensor)
        {
            case TipoSensor.TemperaturaAr:
                minimo = estufa.TemperaturaIdealMin;
                maximo = estufa.TemperaturaIdealMax;
                break;

            case TipoSensor.UmidadeAr:
                minimo = estufa.UmidadeArIdealMin;
                maximo = estufa.UmidadeArIdealMax;
                break;

            case TipoSensor.UmidadeSolo:
                minimo = estufa.UmidadeSoloIdealMin;
                maximo = estufa.UmidadeSoloIdealMax;
                break;

            case TipoSensor.Luminosidade:
                minimo = estufa.LuminosidadeIdealMin;
                maximo = estufa.LuminosidadeIdealMax;
                break;

            case TipoSensor.CO2:
                minimo = estufa.Co2IdealMin;
                maximo = estufa.Co2IdealMax;
                break;

            default:
                return null;
        }

        if (leitura.Valor >= minimo && leitura.Valor <= maximo)
        {
            return null;
        }

        var tipoAlerta = leitura.Valor < minimo ? "Abaixo do ideal" : "Acima do ideal";

        return new AlertaEstufa
        {
            EstufaConfigId = leitura.EstufaConfigId,
            TipoSensor = leitura.TipoSensor,
            TipoAlerta = tipoAlerta,
            NivelCriticidade = DefinirCriticidade(leitura.Valor, minimo, maximo),
            Mensagem = GerarMensagem(leitura.TipoSensor, leitura.Valor, minimo, maximo),
            Recomendacao = GerarRecomendacao(leitura.TipoSensor, leitura.Valor, minimo, maximo),
            Resolvido = false,
            DataCriacao = DateTime.Now
        };
    }

    private static NivelCriticidade DefinirCriticidade(decimal valor, decimal minimo, decimal maximo)
    {
        decimal referencia = valor < minimo ? minimo : maximo;
        decimal diferenca = Math.Abs(valor - referencia);

        if (diferenca <= referencia * 0.10m)
        {
            return NivelCriticidade.Baixo;
        }

        if (diferenca <= referencia * 0.25m)
        {
            return NivelCriticidade.Medio;
        }

        if (diferenca <= referencia * 0.50m)
        {
            return NivelCriticidade.Alto;
        }

        return NivelCriticidade.Critico;
    }

    private static string GerarMensagem(TipoSensor tipoSensor, decimal valor, decimal minimo, decimal maximo)
    {
        return tipoSensor switch
        {
            TipoSensor.TemperaturaAr =>
                $"Temperatura do ar fora do intervalo ideal. Valor recebido: {valor}. Intervalo ideal: {minimo} a {maximo}.",

            TipoSensor.UmidadeAr =>
                $"Umidade do ar fora do intervalo ideal. Valor recebido: {valor}. Intervalo ideal: {minimo} a {maximo}.",

            TipoSensor.UmidadeSolo =>
                $"Umidade do solo fora do intervalo ideal. Valor recebido: {valor}. Intervalo ideal: {minimo} a {maximo}.",

            TipoSensor.Luminosidade =>
                $"Luminosidade fora do intervalo ideal. Valor recebido: {valor}. Intervalo ideal: {minimo} a {maximo}.",

            TipoSensor.CO2 =>
                $"CO2 fora do intervalo ideal. Valor recebido: {valor}. Intervalo ideal: {minimo} a {maximo}.",

            _ =>
                $"Leitura fora do intervalo ideal. Valor recebido: {valor}. Intervalo ideal: {minimo} a {maximo}."
        };
    }

    private static string GerarRecomendacao(TipoSensor tipoSensor, decimal valor, decimal minimo, decimal maximo)
    {
        var direcao = valor < minimo ? "aumentar" : "reduzir";

        return tipoSensor switch
        {
            TipoSensor.TemperaturaAr =>
                $"Recomenda-se {direcao} a temperatura da estufa.",

            TipoSensor.UmidadeAr =>
                $"Recomenda-se {direcao} a umidade do ar.",

            TipoSensor.UmidadeSolo =>
                $"Recomenda-se {direcao} a umidade do solo.",

            TipoSensor.Luminosidade =>
                $"Recomenda-se {direcao} a luminosidade artificial.",

            TipoSensor.CO2 =>
                $"Recomenda-se {direcao} o nível de CO2.",

            _ =>
                "Recomenda-se verificar os parâmetros da estufa."
        };
    }

    private static LeituraSensorRespostaDto MapearParaRespostaDto(LeituraSensor leitura)
    {
        return new LeituraSensorRespostaDto
        {
            Id = leitura.Id,
            EstufaConfigId = leitura.EstufaConfigId,
            TipoSensor = leitura.TipoSensor,
            Valor = leitura.Valor,
            DataLeitura = leitura.DataLeitura
        };
    }
}