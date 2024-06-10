using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaGym2.Datos;
using PruebaGym2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaGym2.Controllers
{
    public class RutinasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RutinasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rutinas
        public async Task<IActionResult> Index()
        {
            var rutinas = await _context.Rutinas
                .Include(r => r.SocioRutinas) // Incluir las relaciones con SocioRutinas
                .ToListAsync();

            foreach (var rutina in rutinas)
            {
                rutina.CalificacionPromedio = CalcularPromedioCalificaciones(rutina);
            }

            return View(rutinas);
        }
        private double CalcularPromedioCalificaciones(Rutina rutina)
        {
            if (rutina.SocioRutinas == null || !rutina.SocioRutinas.Any())
            {
                return 0;
            }

            var calificaciones = rutina.SocioRutinas.Where(sr => sr.Calificacion.HasValue).Select(sr => sr.Calificacion.Value);

            if (!calificaciones.Any())
            {
                return 0;
            }

            return calificaciones.Average();
        }


        // GET: Rutinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rutina = await _context.Rutinas
                .FirstOrDefaultAsync(m => m.IdRutina == id);
            if (rutina == null)
            {
                return NotFound();
            }

            return View(rutina);
        }

        // GET: Rutinas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rutinas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRutina,Descripcion,TipoRutina")] Rutina rutina)
        {
            if (ModelState.IsValid)
            {
                rutina.CalificacionPromedio = CalcularPromedioCalificaciones(rutina);

                _context.Add(rutina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rutina);
        }

        // GET: Rutinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rutina = await _context.Rutinas.FindAsync(id);
            if (rutina == null)
            {
                return NotFound();
            }
            return View(rutina);
        }

        // POST: Rutinas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRutina,Descripcion,TipoRutina,CalificacionPromedio")] Rutina rutina)
        {
            if (id != rutina.IdRutina)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Recalcula el promedio de calificaciones
                    rutina.CalificacionPromedio = CalcularPromedioCalificaciones(rutina);

                    _context.Update(rutina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RutinaExists(rutina.IdRutina))
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
            return View(rutina);
        }

        // GET: Rutinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rutina = await _context.Rutinas
                .FirstOrDefaultAsync(m => m.IdRutina == id);
            if (rutina == null)
            {
                return NotFound();
            }

            return View(rutina);
        }

        // POST: Rutinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rutina = await _context.Rutinas.FindAsync(id);
            if (rutina != null)
            {
                _context.Rutinas.Remove(rutina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RutinaExists(int id)
        {
            return _context.Rutinas.Any(e => e.IdRutina == id);
        }

        // Ver Rutinas y Ejercicios
        public async Task<IActionResult> VerRutinasYEjercicios()
        {
            var rutinasConEjercicios = await _context.Rutinas
                .Include(r => r.RutinaEjercicios)
                .ThenInclude(re => re.Ejercicio)
                .ToListAsync();

            return View(rutinasConEjercicios);
        }
    }
}
