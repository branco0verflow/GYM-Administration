using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaGym2.Models
{
    public class Maquina
    {
        [Key]
        public int IdMaquina { get; set; }

        [Required]
        [Display(Name = "Fecha de compra")]
        [DataType(DataType.Date)]
        public DateTime FechaCompra { get; set; }
        [Column(TypeName = "date")]

        [Required]
        [Display(Name = "Precio de compra")]
        [Range(1, int.MaxValue, ErrorMessage = "El precio de compra debe ser mayor a 0.")]
        public int PrecioCompra { get; set; }

        [Required]
        [Display(Name = "Vida útil")]
        [Range(1, int.MaxValue, ErrorMessage = "La vida útil debe ser mayor a 0.")]
        public int VidaUtil { get; set; }

        [Required]
        [Display(Name = "Disponibilidad")]
        public bool Disponible { get; set; }

        [Display(Name = "Local al que pertenece")]
        [ForeignKey("Local")]
        public int IdLocal { get; set; }
        public Local? Local { get; set; }

        [Display(Name = "Tipo de máquina")]
        [ForeignKey("TipoMaquina")]
        public int IdTipoMaquina { get; set; }
        public TipoMaquina? TipoMaquina { get; set; }
    }
}
