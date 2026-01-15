using System.ComponentModel.DataAnnotations;


namespace Management_Atelier_Reparatii.Models
{
    public class Mecanic
    {
        public int MecanicId { get; set; }
        public string? Nume { get; set; } = string.Empty;
        public string? Specializare { get; set; } = string.Empty;
        public string? Telefon { get; set; }

        public ICollection<ComandaService> ComenziService { get; set; } = new List<ComandaService>();
    }
}
