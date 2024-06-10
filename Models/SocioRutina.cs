using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaGym2.Models
{
    public class SocioRutina
    {
        [Display(Name = "Socio que realiza la rutina")]
        public int IdSocio { get; set; }
        public Socio? Socio { get; set; }

        [Display(Name = "Rutina que realiza el socio")]
        public int IdRutina { get; set; }
        public Rutina? Rutina { get; set; }

        [Display(Name = "Calificacion del socio en la rutina")]
        [Range(1, 10, ErrorMessage = "La calificación debe estar entre 1 y 10.")]
        public int? Calificacion { get; set; }
    }
}
