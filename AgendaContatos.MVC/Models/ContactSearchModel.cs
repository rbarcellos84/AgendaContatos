using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.MVC.Models
{
    public class ContactSearchModel
    {
        public int IdContato { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
    }
}
