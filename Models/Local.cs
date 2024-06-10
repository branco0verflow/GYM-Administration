using PruebaGym2.Datos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaGym2.Models
{
    public class Local
    {
        [Key]
        public int IdLocal { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre del local")]
        public string? Nombre { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Ciudad del local")]
        public string? Ciudad { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Dirección del local")]
        public string? Direccion { get; set; }

        [Required]
        [Display(Name = "Telefono del local")]
        [RegularExpression(@"^\d{1,9}$", ErrorMessage = "El número de teléfono debe tener un máximo de 9 dígitos.")]
        public int Telefono { get; set; }

        public List<Maquina>? MaquinasDeLocal { get; set; }

        [Required]
        [Display(Name = "Nombre del responsable")]
        [ForeignKey("Responsable")]
        public int IdResponsable { get; set; }
        public Responsable? Responsable { get; set; }

        public ICollection<Socio>? SociosAfiliados { get; set; }



    }
}
