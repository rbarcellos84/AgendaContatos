using AgendaContatos.Data.Entities;

namespace AgendaContatos.MVC.Models
{
    public class AuthenticationModel
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataHoraAcesso { get; set; }

        public AuthenticationModel()
        {
            //vazio
        }
        public AuthenticationModel(Usuario dados)
        {
            IdUsuario = dados.IdUsuario;
            Nome = dados.Nome;
            Email = dados.Email;
            DataHoraAcesso = DateTime.Now;
        }
    }
}
