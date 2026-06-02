using GlobalSoluction.Enums;

namespace GlobalSoluction.Models;

public class AlertaEstufa
{
    public int Id { get; set; }

    public int EstufaConfigId { get; set; }

    public EstufaConfig? EstufaConfig { get; set; }

    public int? SensorId { get; set; }

    public Sensor? Sensor { get; set; }

    public string TipoAlerta { get; set; } = string.Empty;

    public NivelCriticidade NivelCriticidade { get; set; } = NivelCriticidade.Baixo;

    public string Mensagem { get; set; } = string.Empty;

    public string Recomendacao { get; set; } = string.Empty;

    public bool Resolvido { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.Now;

    public DateTime? DataResolucao { get; set; }
}