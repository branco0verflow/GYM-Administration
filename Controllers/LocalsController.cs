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
    public class LocalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Locals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Locales.Include(l => l.Responsable);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Locals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _context.Locales
                .Include(l => l.Responsable)
                .FirstOrDefaultAsync(m => m.IdLocal == id);
            if (local == null)
            {
                return NotFound();
            }

            return View(local);
        }

        // GET: Locals/Create
        public IActionResult Create()
        {
            ViewData["IdResponsable"] = new SelectList(_context.Responsables, "idResponsable", "Nombre");
            return View();
        }

        // POST: Locals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLocal,Nombre,Ciudad,Direccion,Telefono,IdResponsable")] Local local)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el responsable ya está asignado a otro local
                var existingLocal = await _context.Locales.FirstOrDefaultAsync(l => l.IdResponsable == local.IdResponsable);
                if (existingLocal != null)
                {
                    ModelState.AddModelError("IdResponsable", "Este responsable ya está asignado a otro local.");
                    ViewData["IdResponsable"] = new SelectList(_context.Responsables, "idResponsable", "Nombre", local.IdResponsable);
                    return View(local);
                }

                _context.Add(local);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si hay un error de validación, volvemos a cargar la lista de responsables en la vista
            ViewData["IdResponsable"] = new SelectList(_context.Responsables, "idResponsable", "Nombre", local.IdResponsable);
            return View(local);
        }

        // GET: Locals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _context.Locales.FindAsync(id);
            if (local == null)
            {
                return NotFound();
            }
            ViewData["IdResponsable"] = new SelectList(_context.Responsables, "idResponsable", "Nombre", local.IdResponsable);
            return View(local);
        }

        // POST: Locals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLocal,Nombre,Ciudad,Direccion,Telefono,IdResponsable")] Local local)
        {
            if (id != local.IdLocal)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Verificar si el responsable ya está asignado a otro local
                var existingLocal = await _context.Locales.FirstOrDefaultAsync(l => l.IdResponsable == local.IdResponsable && l.IdLocal != local.IdLocal);
                if (existingLocal != null)
                {
                    ModelState.AddModelError("IdResponsable", "Este responsable ya está asignado a otro local.");
                    ViewData["IdResponsable"] = new SelectList(_context.Responsables, "idResponsable", "Nombre", local.IdResponsable);
                    return View(local);
                }

                _context.Update(local);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdResponsable"] = new SelectList(_context.Responsables, "idResponsable", "Nombre", local.IdResponsable);
            return View(local);
        }

        // GET: Locals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _context.Locales
                .Include(l => l.Responsable)
                .FirstOrDefaultAsync(m => m.IdLocal == id);
            if (local == null)
            {
                return NotFound();
            }

            return View(local);
        }

        // POST: Locals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var local = await _context.Locales.FindAsync(id);
            if (local != null)
            {
                _context.Locales.Remove(local);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalExists(int id)
        {
            return _context.Locales.Any(e => e.IdLocal == id);
        }
    }
}
