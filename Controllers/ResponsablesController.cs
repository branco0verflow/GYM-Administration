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
    public class ResponsablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResponsablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Responsables
        public async Task<IActionResult> Index()
        {
            return View(await _context.Responsables.ToListAsync());
        }

        // GET: Responsables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsable = await _context.Responsables
                .FirstOrDefaultAsync(m => m.idResponsable == id);
            if (responsable == null)
            {
                return NotFound();
            }

            return View(responsable);
        }

        // GET: Responsables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Responsables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idResponsable,Nombre,Telefono")] Responsable responsable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(responsable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(responsable);
        }

        // GET: Responsables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsable = await _context.Responsables.FindAsync(id);
            if (responsable == null)
            {
                return NotFound();
            }
            return View(responsable);
        }

        // POST: Responsables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idResponsable,Nombre,Telefono")] Responsable responsable)
        {
            if (id != responsable.idResponsable)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(responsable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponsableExists(responsable.idResponsable))
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
            return View(responsable);
        }

        // GET: Responsables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsable = await _context.Responsables
                .FirstOrDefaultAsync(m => m.idResponsable == id);
            if (responsable == null)
            {
                return NotFound();
            }

            return View(responsable);
        }

        // POST: Responsables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var responsable = await _context.Responsables.FindAsync(id);
            if (responsable != null)
            {
                _context.Responsables.Remove(responsable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResponsableExists(int id)
        {
            return _context.Responsables.Any(e => e.idResponsable == id);
        }
    }
}
