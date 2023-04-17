using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.MVC.Models
{
    public class AccountRegisterModel
    {
        [Required (ErrorMessage ="Por favor, informe seu nome.")]
        [MinLength(4, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(200,ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe seu e-mail.")]
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de e-mail valido.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, informe a sua senha.")]
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        public string Senha1 { get; set; }

        [Required(ErrorMessage = "Por favor, confirme a sua senha.")]
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Compare("Senha1",ErrorMessage = "Senha não confere.")]
        public string Senha2 { get; set; }
    }
}
