using System.ComponentModel.DataAnnotations;

namespace ClinicaLucas.Models
{
    public class Paciente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Nascimento { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public Sexo Sexo { get; set; }

        public Paciente()
        {
           
        }


        

    }
}
