using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaGym2.Models
{
    public class Socio
    {
        [Key]
        public int IdSocio { get; set; }

        [Required]
        [Display(Name = "Nombre y apellido del socio")]
        public string? NombreSocio { get; set; }

        [Required]
        [Display(Name = "Tipo de socio")]
        public string? TipoSocio { get; set; } //  ‘estándar’ o ‘premium’

        [Required]
        [Display(Name = "Teléfono del socio")]
        [RegularExpression(@"^\d{1,9}$", ErrorMessage = "El número de teléfono debe tener un máximo de 9 dígitos.")]
        public int? Telefono { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email del socio")]
        public string? Mail { get; set; }

        [Display(Name = "Local al que se insrcribe")]
        [ForeignKey("Local")]
        public int IdLocal { get; set; }
        public Local? Local { get; set; }

        public ICollection<SocioRutina>? SocioRutinas { get; set; }
    }
}
