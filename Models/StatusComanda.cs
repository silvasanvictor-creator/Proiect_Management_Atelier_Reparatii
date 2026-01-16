using System.ComponentModel.DataAnnotations;

namespace Management_Atelier_Reparatii.Models
{
    public enum StatusComanda
    {
        [Display(Name = "Noua")]
        Nou = 0,
        [Display(Name = "In lucru")]
        InLucru = 1,
        [Display(Name = "Finalizata")]
        Finalizat = 2,
        [Display(Name = "Predata")]
        Predat = 3
    }
}