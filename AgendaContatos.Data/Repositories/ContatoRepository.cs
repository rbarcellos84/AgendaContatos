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
    public class ContatoRepository
    {
        public void Create(Contato contato)
        {
            string Sql = @"Insert into Contato (IdUsuario, Nome, Email, Telefone1,Telefone2,Observacao,DataCadastro) 
                                        values (@IdUsuario, @Nome, @Email, @Telefone1, @Telefone2, @Observacao, @DataCadastro)";
            //conectar com o banco
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                //executa comando no sql
                connection.Execute(Sql, contato);
            }
        }
        public void Update(Contato contato)
        {
            string Sql = @"Update Contato set
                                     Nome = @Nome, 
                                    Email = @Email,
                                    Telefone1 = @Telefone1,
                                    Telefone2 = @Telefone2,
                                    Observacao = @Observacao
                          where IdContato = @IdContato
                            and IdUsuario = @IdUsuario";

            //conectar com o banco
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                //executa comando no sql
                connection.Execute(Sql, contato);
            }
        }
        public void Delete(Contato contato)
        {
            string Sql = @"delete from contato where IdContato = @IdContato and IdUsuario = @IdUsuario";

            //conectar com o banco
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                //executa comando no sql
                connection.Execute(Sql, contato);
            }
        }
        public List<Contato> GetByUsuario(int idUsuario)
        {
            string Sql = @"select IdContato, IdUsuario, Nome, Email, Telefone1,Telefone2,Observacao,DataCadastro from Contato where IdUsuario = @idUsuario order by Nome";

            //conectar com o banco
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                //executa comando no sql
                return connection.Query<Contato>(Sql, new { idUsuario }).ToList();
            }
        }
        public Contato GetByIdContato(int idContato, int idUsuario)
        {
            string Sql = @"select IdContato, IdUsuario, Nome, Email, Telefone1,Telefone2,Observacao,DataCadastro from Contato where IdContato = @idContato and IdUsuario = @idUsuario";

            //conectar com o banco
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                //executa comando no sql
                return connection.Query<Contato>(Sql, new { idContato, idUsuario }).FirstOrDefault();
            }
        }
    }
}
