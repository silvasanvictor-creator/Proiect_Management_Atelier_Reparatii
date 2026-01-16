using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Management_Atelier_Reparatii.Models
{
    public class ComandaService
    {
        public int ComandaServiceId { get; set; }

        [Required(ErrorMessage = "Selectati Numarul de inmatriculare"), Display(Name = "NumarInmatriculare")]
        public int MasinaId { get; set; }
        public Masina? Masina { get; set; }

        [Required(ErrorMessage = "Selectati mecanicul"), Display(Name = "Nume Mecanic")]
        public int MecanicId { get; set; }
        public Mecanic? Mecanic { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime DataPrimire { get; set; } = DateTime.Today;

        [Required]
        public StatusComanda Status { get; set; } = StatusComanda.Nou;

        [StringLength(500)]
        public string? Observatii { get; set; }

    }
}