namespace GlobalSoluction.DTOs.LocalOrbital;

public class LocalOrbitalRespostaDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Planeta { get; set; } = string.Empty;

    public string Regiao { get; set; } = string.Empty;

    public bool ProtecaoNatural { get; set; }

    public decimal IncidenciaSolar { get; set; }

    public bool PossuiGeloSubterraneo { get; set; }

    public decimal NivelRiscoRadiacao { get; set; }

    public string Observacoes { get; set; } = string.Empty;

    public DateTime DataAnalise { get; set; }

    public int QuantidadeEstufas { get; set; }
}