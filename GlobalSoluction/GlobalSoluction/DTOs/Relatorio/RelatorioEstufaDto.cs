namespace GlobalSoluction.DTOs.Relatorio;

public class RelatorioEstufaDto
{
    public int TotalLocaisOrbitais { get; set; }
    public int TotalEstufas { get; set; }
    public int TotalLeituras { get; set; }
    public int TotalAlertas { get; set; }
    public int AlertasPendentes { get; set; }
    public int AlertasResolvidos { get; set; }
    public DateTime DataGeracao { get; set; }
}