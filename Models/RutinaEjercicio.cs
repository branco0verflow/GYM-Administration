using System.ComponentModel.DataAnnotations;

namespace PruebaGym2.Models
{
    public class RutinaEjercicio
    {
        [Display(Name = "Rutina")]
        public int IdRutina { get; set; }
        public Rutina? Rutina { get; set; }


        [Display(Name = "Ejercicio")]
        public int IdEjercicio { get; set; }
        public Ejercicio? Ejercicio { get; set; }


        [Display(Name = "Cantidad de sets")]
        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un número válido.")]
        public int Sets { get; set; }

        [Display(Name = "Cantidad de repeticiones")]
        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un número válido.")]
        public int Repeticiones { get; set; }

    }
}
