using System.ComponentModel.DataAnnotations;

namespace ClinicaLucas.Models
{
    public class Exame
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(1000)]
        public string Observacoes { get; set; }

        public int TipoExameId { get; set;}

        public TipoExame? TipoExame { get; set; }

       
    }
}
