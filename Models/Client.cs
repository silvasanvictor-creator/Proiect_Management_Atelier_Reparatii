using System.ComponentModel.DataAnnotations;

namespace Management_Atelier_Reparatii.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Display(Name = "Nume Client")]
        public required string Nume { get; set; } = string.Empty;

        [RegularExpression(@"^(\+4|0)7\d{8}$", ErrorMessage = "Introduceți un număr de telefon valid (ex: 0722123456).")]
        public string? Telefon { get; set; } = null;
        
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        public ICollection <Masina> Masini { get; set; } = new List<Masina>();
    }
}
