using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaGym2.Models
{
    public class Ejercicio
    {
        [Key]
        public int IdEjercicio { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre del ejercicio")]
        public string? NombreEjercicio { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Descripcion del ejercicio")]
        public string? DescripcionEjercicio { get; set; }

        

        [ForeignKey("TipoMaquina")]
        [Display(Name = "Máquina utilizada")]
        public int? TipoMaquinaId { get; set; }
        public TipoMaquina? TipoMaquina { get; set; }


    }
}
