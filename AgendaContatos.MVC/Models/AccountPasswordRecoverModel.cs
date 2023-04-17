using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.MVC.Models
{
    public class AccountPasswordRecoverModel
    {
        [Required(ErrorMessage = "Por favor, informe seu e-mail.")]
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de e-mail valido.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        public string Email { get; set; }
    }
}
