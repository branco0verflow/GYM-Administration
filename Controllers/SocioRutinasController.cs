using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaGym2.Datos;
using PruebaGym2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaGym2.Controllers
{
    public class SocioRutinasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SocioRutinasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SocioRutinas
        public async Task<IActionResult> Index()
        {
            var socioRutinas = _context.SociosRutinas
                .Include(sr => sr.Socio)
                .Include(sr => sr.Rutina);
            return View(await socioRutinas.ToListAsync());
        }

        // GET: SocioRutinas/Details
        public async Task<IActionResult> Details(int? idSocio, int? idRutina)
        {
            if (idSocio == null || idRutina == null)
            {
                return NotFound();
            }

            var socioRutina = await _context.SociosRutinas
                .Include(sr => sr.Socio)
                .Include(sr => sr.Rutina)
                .FirstOrDefaultAsync(m => m.IdSocio == idSocio && m.IdRutina == idRutina);
            if (socioRutina == null)
            {
                return NotFound();
            }

            return View(socioRutina);
        }

        // GET: SocioRutinas/Create
        public IActionResult Create()
        {
            
            ViewData["IdSocio"] = new SelectList(_context.Socios, "IdSocio", "NombreSocio");
            ViewData["IdRutina"] = new SelectList(_context.Rutinas, "IdRutina", "Descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSocio,IdRutina,Calificacion")] SocioRutina socioRutina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socioRutina);
                await _context.SaveChangesAsync();

                // Actualizar el promedio de calificaciones de la rutina
                await ActualizarPromedioCalificacionesRutina(socioRutina.IdRutina);

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSocio"] = new SelectList(_context.Socios, "IdSocio", "NombreSocio", socioRutina.IdSocio);
            ViewData["IdRutina"] = new SelectList(_context.Rutinas, "IdRutina", "Descripcion", socioRutina.IdRutina);
            return View(socioRutina);
        }

        private async Task ActualizarPromedioCalificacionesRutina(int rutinaId)
        {
            var rutina = await _context.Rutinas.FindAsync(rutinaId);
            if (rutina != null)
            {
                var calificaciones = await _context.SociosRutinas.Where(sr => sr.IdRutina == rutinaId).Select(sr => sr.Calificacion).ToListAsync();
                if (calificaciones.Any())
                {
                    rutina.CalificacionPromedio = calificaciones.Average();
                }
                else
                {
                    rutina.CalificacionPromedio = null; // O establecer a un valor predeterminado
                }
                await _context.SaveChangesAsync();
            }
        }

        // GET: SocioRutinas/Edit
        public async Task<IActionResult> Edit(int? idSocio, int? idRutina)
        {
            if (idSocio == null || idRutina == null)
            {
                return NotFound();
            }

            var socioRutina = await _context.SociosRutinas
                .FirstOrDefaultAsync(m => m.IdSocio == idSocio && m.IdRutina == idRutina);
            if (socioRutina == null)
            {
                return NotFound();
            }
            ViewData["IdSocio"] = new SelectList(_context.Socios, "IdSocio", "NombreSocio", socioRutina.IdSocio);
            ViewData["IdRutina"] = new SelectList(_context.Rutinas, "IdRutina", "Descripcion", socioRutina.IdRutina);
            return View(socioRutina);
        }

        // POST: SocioRutinas/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idSocio, int idRutina, [Bind("IdSocio,IdRutina,Calificacion")] SocioRutina socioRutina)
        {
            if (idSocio != socioRutina.IdSocio || idRutina != socioRutina.IdRutina)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socioRutina);
                    await _context.SaveChangesAsync();

                    // Actualizar el promedio de calificaciones de la rutina
                    await ActualizarPromedioCalificacionesRutina(socioRutina.IdRutina);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocioRutinaExists(socioRutina.IdSocio, socioRutina.IdRutina))
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
            ViewData["IdSocio"] = new SelectList(_context.Socios, "IdSocio", "NombreSocio", socioRutina.IdSocio);
            ViewData["IdRutina"] = new SelectList(_context.Rutinas, "IdRutina", "Descripcion", socioRutina.IdRutina);
            return View(socioRutina);
        }

        // GET: SocioRutinas/Delete
        public async Task<IActionResult> Delete(int? idSocio, int? idRutina)
        {
            if (idSocio == null || idRutina == null)
            {
                return NotFound();
            }

            var socioRutina = await _context.SociosRutinas
                .Include(sr => sr.Socio)
                .Include(sr => sr.Rutina)
                .FirstOrDefaultAsync(m => m.IdSocio == idSocio && m.IdRutina == idRutina);
            if (socioRutina == null)
            {
                return NotFound();
            }

            return View(socioRutina);
        }

        // POST: SocioRutinas/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idSocio, int idRutina)
        {
            var socioRutina = await _context.SociosRutinas.FindAsync(idSocio, idRutina);
            _context.SociosRutinas.Remove(socioRutina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocioRutinaExists(int idSocio, int idRutina)
        {
            return _context.SociosRutinas.Any(e => e.IdSocio == idSocio && e.IdRutina == idRutina);
        }
    }
}
