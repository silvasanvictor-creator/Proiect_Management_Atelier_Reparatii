using System.ComponentModel.DataAnnotations;


namespace Management_Atelier_Reparatii.Models
{
    public class Masina
    {
        public int MasinaId { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string AnFabricatie { get; set; } = string.Empty;
        public string NumarInmatriculare { get; set; } = string.Empty;

        public ICollection<ComandaService> ComenziService { get; set; } = new List<ComandaService>();


    }
}
