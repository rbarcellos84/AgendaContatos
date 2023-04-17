using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Data.Entities
{
    public class Contato
    {
        public int IdContato { get; set; }
		public int IdUsuario { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public string Telefone1 { get; set; }
		public string Telefone2 { get; set; }
		public string Observacao { get; set; }
		public DateTime DataCadastro { get; set; }

        //relacionamento / associacao
        public Usuario Usuario { get; set; }

        public Contato() 
        {
            //vazio
        }

        public Contato(int idContato, int idUsuario, string nome, string email, string telefone1, string telefone2, string observacao, DateTime dataCadastro)
        {
            IdContato = idContato;
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
            Telefone1 = telefone1;
            Telefone2 = telefone2;
            Observacao = observacao;
            DataCadastro = dataCadastro;
        }

        public Contato(int idContato, int idUsuario, string nome, string email, string telefone1, string telefone2, string observacao, DateTime dataCadastro, Usuario usuario)
        {
            IdContato = idContato;
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
            Telefone1 = telefone1;
            Telefone2 = telefone2;
            Observacao = observacao;
            DataCadastro = dataCadastro;
            Usuario = usuario;
        }
    }
}
