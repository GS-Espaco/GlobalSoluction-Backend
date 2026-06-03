using System.ComponentModel.DataAnnotations;

namespace GlobalSoluction.Models;


public class EstufaConfig
{
    public int Id { get; set; }

    public int LocalOrbitalId { get; set; }

    public LocalOrbital? LocalOrbital { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required]
    public string TipoPlantacao { get; set; } = string.Empty;

    public decimal TemperaturaIdealMin { get; set; }

    public decimal TemperaturaIdealMax { get; set; }

    public decimal UmidadeArIdealMin { get; set; }

    public decimal UmidadeArIdealMax { get; set; }

    public decimal UmidadeSoloIdealMin { get; set; }

    public decimal UmidadeSoloIdealMax { get; set; }

    public decimal LuminosidadeIdealMin { get; set; }

    public decimal LuminosidadeIdealMax { get; set; }

    public decimal Co2IdealMin { get; set; }

    public decimal Co2IdealMax { get; set; }

    public bool Ativa { get; set; } = true;

    public DateTime DataCriacao { get; set; } = DateTime.Now;

    public DateTime? DataAtualizacao { get; set; }

    public ICollection<Sensor> Sensores { get; set; } = new List<Sensor>();

    public ICollection<AlertaEstufa> Alertas { get; set; } = new List<AlertaEstufa>();
    public ICollection<LeituraSensor> Leituras { get; set; } = new List<LeituraSensor>();
}