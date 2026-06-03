namespace GlobalSoluction.DTOs.Estufa;

public class EstufaRespostaDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string TipoPlantacao { get; set; } = string.Empty;
    public bool Ativa { get; set; }

    public DateTime DataCriacao { get; set; }

    public int LocalOrbitalId { get; set; }
}