using System.ComponentModel.DataAnnotations;

namespace GlobalSoluction.DTOs.Estufa;

public class CriarEstufaDto
{
    [Required]
    public int LocalOrbitalId { get; set; }

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
}