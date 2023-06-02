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
    }
}
