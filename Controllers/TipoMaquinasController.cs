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
    public class TipoMaquinasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoMaquinasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoMaquinas
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoMaquinas.ToListAsync());
        }

        // GET: TipoMaquinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMaquina = await _context.TipoMaquinas
                .FirstOrDefaultAsync(m => m.IdTipoMaquina == id);
            if (tipoMaquina == null)
            {
                return NotFound();
            }

            return View(tipoMaquina);
        }

        // GET: TipoMaquinas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoMaquinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoMaquina,NombreTipo,Descripcion")] TipoMaquina tipoMaquina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoMaquina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoMaquina);
        }

        // GET: TipoMaquinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMaquina = await _context.TipoMaquinas.FindAsync(id);
            if (tipoMaquina == null)
            {
                return NotFound();
            }
            return View(tipoMaquina);
        }

        // POST: TipoMaquinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoMaquina,NombreTipo,Descripcion")] TipoMaquina tipoMaquina)
        {
            if (id != tipoMaquina.IdTipoMaquina)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoMaquina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoMaquinaExists(tipoMaquina.IdTipoMaquina))
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
            return View(tipoMaquina);
        }

        // GET: TipoMaquinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMaquina = await _context.TipoMaquinas
                .FirstOrDefaultAsync(m => m.IdTipoMaquina == id);
            if (tipoMaquina == null)
            {
                return NotFound();
            }

            return View(tipoMaquina);
        }

        // POST: TipoMaquinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoMaquina = await _context.TipoMaquinas.FindAsync(id);
            if (tipoMaquina != null)
            {
                _context.TipoMaquinas.Remove(tipoMaquina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoMaquinaExists(int id)
        {
            return _context.TipoMaquinas.Any(e => e.IdTipoMaquina == id);
        }
    }
}
