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
    public class EjerciciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EjerciciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ejercicios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ejercicios.Include(e => e.TipoMaquina);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ejercicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ejercicio = await _context.Ejercicios
                .Include(e => e.TipoMaquina)
                .FirstOrDefaultAsync(m => m.IdEjercicio == id);
            if (ejercicio == null)
            {
                return NotFound();
            }

            return View(ejercicio);
        }

        // GET: Ejercicios/Create
        public IActionResult Create()
        {
            var tiposMaquina = _context.TipoMaquinas.ToList();
            tiposMaquina.Insert(0, new TipoMaquina { IdTipoMaquina = 0, NombreTipo = "No lleva máquina" }); 
            ViewData["TipoMaquinaId"] = new SelectList(tiposMaquina, "IdTipoMaquina", "NombreTipo");
            return View();
        }

        // POST: Ejercicios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEjercicio,NombreEjercicio,DescripcionEjercicio,TipoMaquinaId")] Ejercicio ejercicio)
        {
            if (ModelState.IsValid)
            {
                if (ejercicio.TipoMaquinaId == 0)
                {
                    ejercicio.TipoMaquinaId = null;
                }

                _context.Add(ejercicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var tipoMaquinaList = new List<SelectListItem>
    {
        new SelectListItem { Text = "No lleva máquina", Value = "" }
    };

            tipoMaquinaList.AddRange(new SelectList(_context.TipoMaquinas, "IdTipoMaquina", "NombreTipo"));
            ViewBag.TipoMaquinaId = tipoMaquinaList;

            return View(ejercicio);
        }

        // GET: Ejercicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ejercicio = await _context.Ejercicios.FindAsync(id);
            if (ejercicio == null)
            {
                return NotFound();
            }

            // Obtener la lista de tipos de máquinas
            var tiposMaquina = _context.TipoMaquinas.ToList();

            // Insertar la opción "No lleva máquina" al inicio de la lista
            tiposMaquina.Insert(0, new TipoMaquina { IdTipoMaquina = 0, NombreTipo = "No lleva máquina" });

            // Crear la lista de elementos select para la vista
            ViewData["TipoMaquinaId"] = new SelectList(tiposMaquina, "IdTipoMaquina", "NombreTipo", ejercicio.TipoMaquinaId);

            return View(ejercicio);
        }

        // POST: Ejercicios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEjercicio,NombreEjercicio,DescripcionEjercicio,TipoMaquinaId")] Ejercicio ejercicio)
        {
            if (id != ejercicio.IdEjercicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Si el TipoMaquinaId es 0 (No lleva máquina), establecerlo a null
                    if (ejercicio.TipoMaquinaId == 0)
                    {
                        ejercicio.TipoMaquinaId = null;
                    }

                    _context.Update(ejercicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EjercicioExists(ejercicio.IdEjercicio))
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

            // Si hay un error en el modelo, volver a cargar la lista de tipos de máquinas
            var tiposMaquina = _context.TipoMaquinas.ToList();
            tiposMaquina.Insert(0, new TipoMaquina { IdTipoMaquina = 0, NombreTipo = "No lleva máquina" });
            ViewData["TipoMaquinaId"] = new SelectList(tiposMaquina, "IdTipoMaquina", "NombreTipo", ejercicio.TipoMaquinaId);

            return View(ejercicio);
        }

        // GET: Ejercicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ejercicio = await _context.Ejercicios
                .Include(e => e.TipoMaquina)
                .FirstOrDefaultAsync(m => m.IdEjercicio == id);
            if (ejercicio == null)
            {
                return NotFound();
            }

            return View(ejercicio);
        }

        // POST: Ejercicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ejercicio = await _context.Ejercicios.FindAsync(id);
            if (ejercicio != null)
            {
                _context.Ejercicios.Remove(ejercicio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EjercicioExists(int id)
        {
            return _context.Ejercicios.Any(e => e.IdEjercicio == id);
        }
    }

}
