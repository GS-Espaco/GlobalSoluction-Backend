using GlobalSoluction.Enums;
using System.ComponentModel.DataAnnotations;

namespace GlobalSoluction.Models;

public class Sensor
{
    public int Id { get; set; }

    public int EstufaConfigId { get; set; }

    public EstufaConfig? EstufaConfig { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;

    public TipoSensor TipoSensor { get; set; } = TipoSensor.TemperaturaAr;

    [Required]
    public string UnidadeMedida { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;

    public DateTime DataInstalacao { get; set; } = DateTime.Now;

    public ICollection<LeituraSensor> Leituras { get; set; } = new List<LeituraSensor>();

    public ICollection<AlertaEstufa> Alertas { get; set; } = new List<AlertaEstufa>();
}