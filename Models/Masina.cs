using System.ComponentModel.DataAnnotations;


namespace Management_Atelier_Reparatii.Models
{
    public class Masina
    {
        public int MasinaId { get; set; }
        [Required(ErrorMessage = "Selectati Client"), Display(Name = "Client"), ]
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        [Required]
        public string Marca { get; set; } = string.Empty;
        [Required]
        public string Model { get; set; } = string.Empty;
        [Required, Display(Name = "An Fabricatie")]
        public string AnFabricatie { get; set; } = string.Empty;
        [Required, Display(Name = "Numar Inmatriculare")]
        public string NumarInmatriculare { get; set; } = string.Empty;

        public ICollection<ComandaService> ComenziService { get; set; } = new List<ComandaService>();


    }
}
