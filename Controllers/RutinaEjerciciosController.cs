using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaGym2.Datos;
using PruebaGym2.Models;

namespace PruebaGym2.Controllers
{
    public class RutinaEjerciciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RutinaEjerciciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RutinaEjercicios
        public async Task<IActionResult> Index()
        {
            var rutinaEjercicios = await _context.RutinaEjercicios
                .Include(re => re.Ejercicio)
                .Include(re => re.Rutina)
                .ToListAsync();

            return View(rutinaEjercicios);
        }

        // GET: RutinaEjercicios/Details/5
        public async Task<IActionResult> Details(int? idRutina, int? idEjercicio)
        {
            if (idRutina == null || idEjercicio == null)
            {
                return NotFound();
            }

            var rutinaEjercicio = await _context.RutinaEjercicios
                .Include(re => re.Ejercicio)
                .Include(re => re.Rutina)
                .FirstOrDefaultAsync(re => re.IdRutina == idRutina && re.IdEjercicio == idEjercicio);

            if (rutinaEjercicio == null)
            {
                return NotFound();
            }

            return View(rutinaEjercicio);
        }

        // GET: RutinaEjercicios/Create
        public IActionResult Create()
        {
            ViewData["IdEjercicio"] = new SelectList(_context.Ejercicios, "IdEjercicio", "NombreEjercicio");
            ViewData["IdRutina"] = new SelectList(_context.Rutinas, "IdRutina", "Descripcion");
            return View();
        }

        // POST: RutinaEjercicios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRutina,IdEjercicio,Repeticiones,Sets")] RutinaEjercicio rutinaEjercicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rutinaEjercicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEjercicio"] = new SelectList(_context.Ejercicios, "IdEjercicio", "NombreEjercicio", rutinaEjercicio.IdEjercicio);
            ViewData["IdRutina"] = new SelectList(_context.Rutinas, "IdRutina", "Descripcion", rutinaEjercicio.IdRutina);
            return View(rutinaEjercicio);
        }

        // GET: RutinaEjercicios/Edit/5
        public async Task<IActionResult> Edit(int? idRutina, int? idEjercicio)
        {
            if (idRutina == null || idEjercicio == null)
            {
                return NotFound();
            }

            var rutinaEjercicio = await _context.RutinaEjercicios.FindAsync(idRutina, idEjercicio);
            if (rutinaEjercicio == null)
            {
                return NotFound();
            }

            ViewData["IdEjercicio"] = new SelectList(_context.Ejercicios, "IdEjercicio", "NombreEjercicio", rutinaEjercicio.IdEjercicio);
            ViewData["IdRutina"] = new SelectList(_context.Rutinas, "IdRutina", "Descripcion", rutinaEjercicio.IdRutina);
            return View(rutinaEjercicio);
        }

        // POST: RutinaEjercicios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idRutina, int idEjercicio, [Bind("IdRutina,IdEjercicio,Repeticiones,Sets")] RutinaEjercicio rutinaEjercicio)
        {
            if (idRutina != rutinaEjercicio.IdRutina || idEjercicio != rutinaEjercicio.IdEjercicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rutinaEjercicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RutinaEjercicioExists(rutinaEjercicio.IdRutina, rutinaEjercicio.IdEjercicio))
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
            ViewData["IdEjercicio"] = new SelectList(_context.Ejercicios, "IdEjercicio", "NombreEjercicio", rutinaEjercicio.IdEjercicio);
            ViewData["IdRutina"] = new SelectList(_context.Rutinas, "IdRutina", "Descripcion", rutinaEjercicio.IdRutina);
            return View(rutinaEjercicio);
        }

        // GET: RutinaEjercicios/Delete/5
        public async Task<IActionResult> Delete(int? idRutina, int? idEjercicio)
        {
            if (idRutina == null || idEjercicio == null)
            {
                return NotFound();
            }

            var rutinaEjercicio = await _context.RutinaEjercicios
                .Include(r => r.Ejercicio)
                .Include(r => r.Rutina)
                .FirstOrDefaultAsync(m => m.IdRutina == idRutina && m.IdEjercicio == idEjercicio);
            if (rutinaEjercicio == null)
            {
                return NotFound();
            }

            return View(rutinaEjercicio);
        }

        // POST: RutinaEjercicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idRutina, int idEjercicio)
        {
            var rutinaEjercicio = await _context.RutinaEjercicios.FindAsync(idRutina, idEjercicio);
            if (rutinaEjercicio != null)
            {
                _context.RutinaEjercicios.Remove(rutinaEjercicio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RutinaEjercicioExists(int idRutina, int idEjercicio)
        {
            return _context.RutinaEjercicios.Any(re => re.IdRutina == idRutina && re.IdEjercicio == idEjercicio);
        }
    }
}
