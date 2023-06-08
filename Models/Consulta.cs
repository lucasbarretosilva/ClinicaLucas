namespace ClinicaLucas.Models
{
    public class Consulta
    {
        public int Id { get; set; } 

        public DateTime Data { get; set;}

        public Guid? Protocolo { get; set; }

        public int PacienteId { get; set; }

        public int ExameId { get; set; }

        public Paciente? Paciente { get; set; }

        public Exame? Exame { get; set; }

        public Consulta(int id, DateTime data, int pacienteId, int exameId, Paciente? paciente, Exame? exame)
        {
            Id = id;
            Data = data;
            Protocolo = Guid.NewGuid();
            PacienteId = pacienteId;
            ExameId = exameId;
            Paciente = paciente;
            Exame = exame;
        }

        public Consulta()
        {

        }
    }
}
