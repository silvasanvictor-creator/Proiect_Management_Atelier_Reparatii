using System.ComponentModel.DataAnnotations;


namespace Management_Atelier_Reparatii.Models
{
    public class Mecanic
    {
        public int MecanicId { get; set; }
        public string? Nume { get; set; } = string.Empty;
        public string? Specializare { get; set; } = string.Empty;
        [Required, RegularExpression(@"^(\+4|0)7\d{8}$", ErrorMessage = "Introduceți un număr de telefon valid (ex: 0722123456).")]
        public string? Telefon { get; set; }

        public ICollection<ComandaService> ComenziService { get; set; } = new List<ComandaService>();
    }
}
