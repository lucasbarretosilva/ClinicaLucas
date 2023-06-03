using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClinicaLucas.Models;

namespace ClinicaLucas.Data
{
    public class ClinicaLucasContext : DbContext
    {
        public ClinicaLucasContext (DbContextOptions<ClinicaLucasContext> options)
            : base(options)
        {
        }

        public DbSet<ClinicaLucas.Models.Paciente> Paciente { get; set; } = default!;

        public DbSet<ClinicaLucas.Models.TipoExame>? TipoExame { get; set; }

        public DbSet<ClinicaLucas.Models.Exame>? Exame { get; set; }

        public DbSet<ClinicaLucas.Models.Consulta>? Consulta { get; set; }
    }
}
