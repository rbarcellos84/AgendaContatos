using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Data.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }

        //relacionamento / associacao
        public List<Contato> Contatos { get; set; }
        public Usuario()
        {
            //vazio
        }
        public Usuario(int idUsuario, string nome, string email, string senha, DateTime dataCadastro)
        {
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
            Senha = senha;
            DataCadastro = dataCadastro;
        }
        public Usuario(int idUsuario, string nome, string email, string senha, DateTime dataCadastro, List<Contato> contatos)
        {
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
            Senha = senha;
            DataCadastro = dataCadastro;
            Contatos = contatos;
        }
    }
}
