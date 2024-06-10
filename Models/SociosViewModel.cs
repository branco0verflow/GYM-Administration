using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PruebaGym2.Models
{
    public class SociosViewModel
    {
        public string TipoSocio { get; set; }
        public List<SelectListItem> TiposDeSocios { get; set; }
        public IEnumerable<Socio> Socios { get; set; }
    }
}

