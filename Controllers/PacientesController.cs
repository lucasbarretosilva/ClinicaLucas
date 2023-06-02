using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaLucas.Data;
using ClinicaLucas.Models;

namespace ClinicaLucas.Controllers
{
    public class PacientesController : Controller
    {
        private readonly ClinicaLucasContext _context;

        public PacientesController(ClinicaLucasContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var pacientes = await _context.Paciente.ToListAsync();
            return View(pacientes);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente
                .FirstOrDefaultAsync(m => m.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        
        public IActionResult Create()
        {
            ViewBag.Sexos = SexoEnum();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cpf,Nascimento,Telefone,Email,SexoPaciente")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Sexos = SexoEnum();
            return View(paciente);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            ViewBag.Sexos = SexoEnum();
            return View(paciente);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cpf,Nascimento,Telefone,Email,SexoPaciente")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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

            ViewBag.Sexos = SexoEnum();
            return View(paciente);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente
                .FirstOrDefaultAsync(m => m.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente != null)
            {
                _context.Paciente.Remove(paciente);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Paciente.Any(e => e.Id == id);
        }

        private IEnumerable<SelectListItem> SexoEnum()
        {
            return Enum.GetValues(typeof(Sexo)).Cast<Sexo>().Select(s => new SelectListItem
            {
                Text = s.ToString(),
                Value = s.ToString()
            });
        }
    }
}
