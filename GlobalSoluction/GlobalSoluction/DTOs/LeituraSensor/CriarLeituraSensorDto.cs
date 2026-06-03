using GlobalSoluction.Enums;
using System.ComponentModel.DataAnnotations;

namespace GlobalSoluction.DTOs.LeituraSensor;

public class CriarLeituraSensorDto
{
    [Required]
    public int EstufaConfigId { get; set; }

    [Required]
    public TipoSensor TipoSensor { get; set; }

    [Required]
    public decimal Valor { get; set; }
}