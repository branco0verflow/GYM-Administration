using System.ComponentModel.DataAnnotations;

namespace PruebaGym2.Models
{
    public class Rutina
    {
        [Key]
        public int IdRutina { get; set; }

        [Required]
        [Display(Name = "Descripción de la rutina")]
        public string? Descripcion { get; set; }
        
        [Required]
        [Display(Name = "Tipo de rutina")]
        public string? TipoRutina { get; set; } // “salud”, “competición amateur”, “competición profesional”.

        public List<Ejercicio>? EjerciciosRutina { get; set; } = new List<Ejercicio>();

        [Display(Name = "Calificación promedio obtenida en la rutina")]
        public double? CalificacionPromedio { get; set; }


        public ICollection<SocioRutina>? SocioRutinas { get; set; }

        public ICollection<RutinaEjercicio>? RutinaEjercicios { get; set; }

    }
}
