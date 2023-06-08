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
    public class ConsultasController : Controller
    {
        private readonly ClinicaLucasContext _context;

        public ConsultasController(ClinicaLucasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var consultas = await _context.Consulta
                .Include(c => c.Exame)
                .Include(c => c.Paciente)
                .ToListAsync();

            return View(consultas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta
                .Include(c => c.Exame)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        public IActionResult Create(int? pacienteId)
        {
            if (pacienteId == null)
            {
                return NotFound();
            }

            ViewData["PacienteId"] = new SelectList(_context.Paciente, "Id", "Nome", pacienteId);

            ViewData["ExameId"] = new SelectList(_context.Exame, "Id", "Nome");

            ViewBag.PacienteId = pacienteId;


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,PacienteId,ExameId")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                consulta.Protocolo = Guid.NewGuid();
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ExameId"] = new SelectList(_context.Exame, "Id", "Nome", consulta.ExameId);
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "Id", "Nome", consulta.PacienteId);

            return View(consulta);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }

            ViewData["ExameId"] = new SelectList(_context.Exame, "Id", "Nome", consulta.ExameId);
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "Id", "Nome", consulta.PacienteId);

            return View(consulta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,PacienteId,ExameId")] Consulta consulta)
        {
            if (id != consulta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaExists(consulta.Id))
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

            ViewData["ExameId"] = new SelectList(_context.Exame, "Id", "Nome", consulta.ExameId);
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "Id", "Nome", consulta.PacienteId);

            return View(consulta);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta
                .Include(c => c.Exame)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }

            _context.Consulta.Remove(consulta);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(int id)
        {
            return _context.Consulta.Any(e => e.Id == id);
        }
    }
}
