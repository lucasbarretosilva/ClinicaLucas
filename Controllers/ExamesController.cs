using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaLucas.Data;
using ClinicaLucas.Models;

namespace ClinicaLucas.Controllers
{
    public class ExamesController : Controller
    {
        private readonly ClinicaLucasContext _context;

        public ExamesController(ClinicaLucasContext context)
        {
            _context = context;
        }

        // GET: Exames
        public async Task<IActionResult> Index()
        {
            var clinicaLucasContext = _context.Exame.Include(e => e.TipoExame);
            ViewData["TipoExameId"] = new SelectList(_context.TipoExame, "Id", "Nome");
            return View(await clinicaLucasContext.ToListAsync());
        }

        // GET: Exames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exame == null)
            {
                return NotFound();
            }

            var exame = await _context.Exame
                .Include(e => e.TipoExame)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exame == null)
            {
                return NotFound();
            }

            return View(exame);
        }

        public IActionResult Create()
        {
            ViewData["TipoExameId"] = new SelectList(_context.TipoExame, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Observacoes,TipoExameId")] Exame exame)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["TipoExameId"] = new SelectList(_context.TipoExame, "Id", "Nome");
            return View(exame);
        }

        // GET: Exames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exame == null)
            {
                return NotFound();
            }

            var exame = await _context.Exame.FindAsync(id);
            if (exame == null)
            {
                return NotFound();
            }
            ViewData["TipoExameId"] = new SelectList(_context.TipoExame, "Id", "Nome");
            return View(exame);
        }

        // POST: Exames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Observacoes,TipoExameId")] Exame exame)
        {
            if (id != exame.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExameExists(exame.Id))
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
            //ViewData["TipoExameId"] = new SelectList(_context.TipoExame, "Id", "Nome", exame.TipoExameId);
            ViewData["TipoExameId"] = new SelectList(_context.TipoExame, "Id", "Nome", exame.TipoExameId);

            return View(exame);
        }

        // GET: Exames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exame == null)
            {
                return NotFound();
            }

            var exame = await _context.Exame
                .Include(e => e.TipoExame)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exame == null)
            {
                return NotFound();
            }

            return View(exame);
        }

        // POST: Exames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exame == null)
            {
                return Problem("Entity set 'ClinicaLucasContext.Exame'  is null.");
            }
            var exame = await _context.Exame.FindAsync(id);
            if (exame != null)
            {
                _context.Exame.Remove(exame);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExameExists(int id)
        {
          return (_context.Exame?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
