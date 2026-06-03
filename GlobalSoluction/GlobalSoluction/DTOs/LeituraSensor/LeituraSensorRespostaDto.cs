using GlobalSoluction.Enums;

namespace GlobalSoluction.DTOs.LeituraSensor;

public class LeituraSensorRespostaDto
{
    public int Id { get; set; }

    public int EstufaConfigId { get; set; }

    public TipoSensor TipoSensor { get; set; }

    public decimal Valor { get; set; }

    public DateTime DataLeitura { get; set; }
}