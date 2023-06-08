using System.ComponentModel.DataAnnotations;

namespace ClinicaLucas.Models
{
    public class Paciente
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

       // [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido.")]
        public string Cpf { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Nascimento { get; set; }

        [Required(ErrorMessage = "O campo Número de Celular é obrigatório.")]
        [RegularExpression(@"^\([1-9]{2}\) [9]{1}[6-9]{1}[0-9]{3}\-[0-9]{4}$", ErrorMessage = "O campo Número de Celular deve estar no formato (99) 99999-9999.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo E-mail não é um endereço de e-mail válido.")]
        public string Email { get; set; }

        public Sexo Sexo { get; set; }

        public Paciente()
        {
           
        }


        

    }
}
