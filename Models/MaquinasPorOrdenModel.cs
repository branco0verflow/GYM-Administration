using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PruebaGym2.Models
{
    public class MaquinasPorOrdenModel
    {
        public string TipoOrden { get; set; } // Cambiar nombre de la propiedad

        public List<SelectListItem> OrdenList { get; set; }

        public List<Maquina> Maquinas { get; set; }
        public int VidaUtilRestante { get; set; }
    }
}
