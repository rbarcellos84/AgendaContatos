using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaContatos.Data.Configurations;
using AgendaContatos.Data.Entities;
using Dapper;

namespace AgendaContatos.Data.Repositories
{
    public class UsuarioRepository
    {
        public void Create(Usuario usuario)
        {
            string Sql = @"Insert into Usuario (Nome, Email, Senha, DataCadastro) 
                                        values (@Nome, @Email, CONVERT(VARCHAR(32),HASHBYTES('MD5','" + usuario.Senha + "'),2), @DataCadastro)";
            //conectar com o banco
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                //executa comando no sql
                connection.Execute(Sql, usuario);
            }
        }
        public void Update(Usuario usuario)
        {
            string Sql = @"Update Usuario set 
                                         Nome = @Nome, 
                                        Senha = CONVERT(VARCHAR(32),HASHBYTES('MD5','" + usuario.Senha + "'),2) " + 
                             "where IdUsuario = @IdUsuario";

            //conectar com o banco
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                //executa comando no sql
                connection.Execute(Sql, usuario);
            }
        }
        public void Delete(Usuario usuario)
        {
            string Sql = @"delete from Usuario where IdUsuario = @IdUsuario";

            //conectar com o banco
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                //executa comando no sql
                connection.Execute(Sql, usuario);
            }
        }
        public Usuario GetById(int idUsuario)
        {
            string Sql = @"select IdUsuario, Nome, Email, Senha, DataCadastro from Usuario where IdUsuario = @idUsuario";

            //conectar com o banco
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                //executa comando no sql
                return connection.Query<Usuario>(Sql, new { idUsuario }).FirstOrDefault();
            }
        }
        public Usuario GetByEmail(string email)
        {
            string Sql = @"select IdUsuario, Nome, Email, Senha, DataCadastro from Usuario where email = '" + email + "'";

            //conectar com o banco
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                //executa comando no sql
                return connection.Query<Usuario>(Sql, new {}).FirstOrDefault();
            }
        }
        public Usuario GetByLogin(string email, string senha)
        {
            string Sql = @"select IdUsuario, Nome, Email, Senha, DataCadastro from Usuario where Email = '" + email + "' and Senha = CONVERT(VARCHAR(32),HASHBYTES('MD5','" + senha + "'),2)";

            //conectar com o banco
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                //executa comando no sql
                return connection.Query<Usuario>(Sql, new { }).FirstOrDefault();
            }
        }
        public List<Usuario> GetAll()
        {
            string Sql = @"select IdUsuario, Nome, Email, Senha, DataCadastro from Usuario order by Nome";

            //conectar com o banco
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                //executa comando no sql
                return connection.Query<Usuario>(Sql).ToList();
            }
        }
    }
}
