using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.MVC.Models
{
    public class ContactRegisterModel
    {
        public int? IdContato { get; set; }

        [Required(ErrorMessage = "Por favor, informe seu nome.")]
        [MinLength(4, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(200, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        public string Nome { get; set; }
        public string? Email { get; set; }

        [RegularExpression("^[0-9]{11}$", ErrorMessage = "Por favor, informe no mínimo 11 digitos numéricos")]
        [Required(ErrorMessage = "Por favor, informe o número de telefone.")]
        public string Telefone1 { get; set; }
        public string? Telefone2 { get; set; }
        public string? Observacao { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
