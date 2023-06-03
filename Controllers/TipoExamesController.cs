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
    public class TipoExamesController : Controller
    {
        private readonly ClinicaLucasContext _context;

        public TipoExamesController(ClinicaLucasContext context)
        {
            _context = context;
        }

        // GET: TipoExames
        public async Task<IActionResult> Index()
        {
              return _context.TipoExame != null ? 
                          View(await _context.TipoExame.ToListAsync()) :
                          Problem("Entity set 'ClinicaLucasContext.TipoExame'  is null.");
        }

        // GET: TipoExames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoExame == null)
            {
                return NotFound();
            }

            var tipoExame = await _context.TipoExame
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoExame == null)
            {
                return NotFound();
            }

            return View(tipoExame);
        }

        // GET: TipoExames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoExames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao")] TipoExame tipoExame)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoExame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoExame);
        }

        // GET: TipoExames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoExame == null)
            {
                return NotFound();
            }

            var tipoExame = await _context.TipoExame.FindAsync(id);
            if (tipoExame == null)
            {
                return NotFound();
            }
            return View(tipoExame);
        }

        // POST: TipoExames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao")] TipoExame tipoExame)
        {
            if (id != tipoExame.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoExame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoExameExists(tipoExame.Id))
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
            return View(tipoExame);
        }

        // GET: TipoExames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoExame == null)
            {
                return NotFound();
            }

            var tipoExame = await _context.TipoExame
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoExame == null)
            {
                return NotFound();
            }

            return View(tipoExame);
        }

        // POST: TipoExames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoExame == null)
            {
                return Problem("Entity set 'ClinicaLucasContext.TipoExame'  is null.");
            }
            var tipoExame = await _context.TipoExame.FindAsync(id);
            if (tipoExame != null)
            {
                _context.TipoExame.Remove(tipoExame);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoExameExists(int id)
        {
          return (_context.TipoExame?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
