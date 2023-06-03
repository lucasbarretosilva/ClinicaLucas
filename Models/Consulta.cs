namespace ClinicaLucas.Models
{
    public class Consulta
    {
        public int Id { get; set; } 

        public DateTime Data { get; set;}

        public Guid Protocolo { get; set; }

        public int PacienteId { get; set; }

        public int ExameId { get; set; }

        public Paciente? Paciente { get; set; }

        public Exame Exame { get; set; }


    }
}
