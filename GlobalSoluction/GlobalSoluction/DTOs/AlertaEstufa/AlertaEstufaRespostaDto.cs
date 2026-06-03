using GlobalSoluction.Enums;

namespace GlobalSoluction.DTOs.AlertaEstufa;

public class AlertaEstufaRespostaDto
{
    public int Id { get; set; }

    public int EstufaConfigId { get; set; }

    public TipoSensor TipoSensor { get; set; }

    public string TipoAlerta { get; set; } = string.Empty;

    public NivelCriticidade NivelCriticidade { get; set; }

    public string Mensagem { get; set; } = string.Empty;

    public string Recomendacao { get; set; } = string.Empty;

    public bool Resolvido { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataResolucao { get; set; }
}