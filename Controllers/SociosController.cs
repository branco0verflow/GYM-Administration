using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaGym2.Datos;
using PruebaGym2.Models;

namespace PruebaGym2.Controllers
{
    public class SociosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SociosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Socios
        public async Task<IActionResult> Index(string tipoSocio)
        {
            var socios = _context.Socios.Include(s => s.Local).AsQueryable();

            if (!string.IsNullOrEmpty(tipoSocio))
            {
                socios = socios.Where(s => s.TipoSocio == tipoSocio);
            }

            var model = new SociosViewModel
            {
                TipoSocio = tipoSocio,
                TiposDeSocios = new List<SelectListItem>
                {
                    new SelectListItem { Value = "", Text = "Todos" },
                    new SelectListItem { Value = "Estándar", Text = "Estándar" },
                    new SelectListItem { Value = "Premium", Text = "Premium" }
                },
                Socios = await socios.ToListAsync()
            };

            return View(model);
        }

        

        // GET: Socios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios
                .FirstOrDefaultAsync(m => m.IdSocio == id);
            if (socio == null)
            {
                return NotFound();
            }

            return View(socio);
        }

        // GET: Socios/Create
        public IActionResult Create()
        {
            ViewBag.Locales = _context.Locales.ToList();
            return View();
        }

        // POST: Socios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSocio,NombreSocio,TipoSocio,Telefono,Mail,IdLocal")] Socio socio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socio);
                await _context.SaveChangesAsync();

                // Obtener la rutina asociada a este socio
                var socioRutina = await _context.SociosRutinas
                    .FirstOrDefaultAsync(sr => sr.IdSocio == socio.IdSocio);

                if (socioRutina != null)
                {
                    // Obtener la rutina correspondiente
                    var rutina = await _context.Rutinas.FindAsync(socioRutina.IdRutina);

                    if (rutina != null)
                    {
                        // Actualizar el promedio de calificaciones de la rutina
                        rutina.CalificacionPromedio = CalcularPromedioCalificacionesRutina(rutina.IdRutina);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Locales = _context.Locales.ToList();
            return View(socio);
        }

        private double CalcularPromedioCalificacionesRutina(int rutinaId)
        {
            var calificaciones = _context.SociosRutinas
                .Where(sr => sr.IdRutina == rutinaId && sr.Calificacion.HasValue)
                .Select(sr => sr.Calificacion.Value);

            return calificaciones.Any() ? calificaciones.Average() : 0;
        }

        // GET: Socios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios.FindAsync(id);
            if (socio == null)
            {
                return NotFound();
            }

            ViewBag.Locales = _context.Locales.ToList();
            return View(socio);
        }

        // POST: Socios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSocio,NombreSocio,TipoSocio,Telefono,Mail,IdLocal")] Socio socio)
        {
            if (id != socio.IdSocio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocioExists(socio.IdSocio))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Locales = _context.Locales.ToList();
            return View(socio);
        }

        // GET: Socios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios
                .FirstOrDefaultAsync(m => m.IdSocio == id);
            if (socio == null)
            {
                return NotFound();
            }

            return View(socio);
        }

        // POST: Socios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var socio = await _context.Socios.FindAsync(id);
            if (socio != null)
            {
                _context.Socios.Remove(socio);
                await _context.SaveChangesAsync();

                // Obtener la rutina asociada a este socio
                var socioRutina = await _context.SociosRutinas.FirstOrDefaultAsync(sr => sr.IdSocio == id);

                if (socioRutina != null)
                {
                    // Obtener la rutina correspondiente
                    var rutina = await _context.Rutinas.FindAsync(socioRutina.IdRutina);

                    if (rutina != null)
                    {
                        // Actualizar el promedio de calificaciones de la rutina
                        rutina.CalificacionPromedio = CalcularPromedioCalificacionesRutina(rutina.IdRutina);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SocioExists(int id)
        {
            return _context.Socios.Any(e => e.IdSocio == id);
        }
    }
}
