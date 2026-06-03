using GlobalSoluction.Enums;

namespace GlobalSoluction.Models;

public class LeituraSensor
{
    public int Id { get; set; }

    public int EstufaConfigId { get; set; }

    public EstufaConfig? EstufaConfig { get; set; }

    public TipoSensor TipoSensor { get; set; }

    public decimal Valor { get; set; }

    public DateTime DataLeitura { get; set; } = DateTime.Now;
}