using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaLucas.Data;
using ClinicaLucas.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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

            var paciente = await _context.Paciente.FirstOrDefaultAsync(m => m.Id == id);

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
        public async Task<IActionResult> Create([Bind("Id,Nome,Cpf,Nascimento,Telefone,Email,Sexo")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                if (!IsCpfValid(paciente.Cpf))
                {
                    ModelState.AddModelError("Cpf", "CPF inválido.");
                }
                else
                {
                    var cpfExists = await _context.Paciente.AnyAsync(p => p.Cpf == paciente.Cpf);
                    if (cpfExists)
                    {
                        ModelState.AddModelError("Cpf", "CPF já cadastrado.");
                    }
                    else
                    {
                        _context.Add(paciente);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cpf,Nascimento,Telefone,Email,Sexo")] Paciente paciente)
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

            var paciente = await _context.Paciente.FirstOrDefaultAsync(m => m.Id == id);

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

        [HttpPost]
        public async Task<IActionResult> Search(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                var pacientes = await _context.Paciente.ToListAsync();
                return PartialView("_PacientesTablePartial", pacientes);
            }

            var pacientesFiltrados = await _context.Paciente
                .Where(p => p.Nome.Contains(searchString) || p.Cpf.Contains(searchString))
                .ToListAsync();

            return PartialView("_PacientesTablePartial", pacientesFiltrados);
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

        private bool IsCpfValid(string cpf)
        {
           
            cpf = Regex.Replace(cpf, "[^0-9]", "");

           
            if (cpf.Length != 11)
                return false;

            
            if (new string(cpf[0], 11) == cpf)
                return false;

            
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (cpf[i] - '0') * (10 - i);
            int digito1 = 11 - (soma % 11);
            if (digito1 >= 10)
                digito1 = 0;

            
            if (cpf[9] - '0' != digito1)
                return false;

           
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (cpf[i] - '0') * (11 - i);
            int digito2 = 11 - (soma % 11);
            if (digito2 >= 10)
                digito2 = 0;

            
            if (cpf[10] - '0' != digito2)
                return false;

            return true;
        }


        




    }
}
