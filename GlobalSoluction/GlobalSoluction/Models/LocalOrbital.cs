using System.ComponentModel.DataAnnotations;

namespace GlobalSoluction.Models
{
   public class LocalOrbital
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Planeta { get; set; } = string.Empty;

        [Required]
        public string Regiao { get; set; } = string.Empty;

        public bool ProtecaoNatural { get; set; }

        public decimal IncidenciaSolar { get; set; }

        public bool PossuiGeloSubterraneo { get; set; }

        public decimal NivelRiscoRadiacao { get; set; }

        public string Observacoes { get; set; } = string.Empty;

        public DateTime DataAnalise { get; set; } = DateTime.Now;

        public ICollection<EstufaConfig> Estufas { get; set; } = new List<EstufaConfig>();
    }
}
