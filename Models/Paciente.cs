namespace ClinicaLucas.Models
{
    public class Paciente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public DateTime Nascimento { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public Sexo Sexo { get; set; }

        public Paciente()
        {
            Sexo = Sexo.Masculino;
        }


    }
}
