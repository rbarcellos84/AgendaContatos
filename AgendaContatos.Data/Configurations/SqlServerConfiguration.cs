using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Data.Configurations
{
    public class SqlServerConfiguration
    {
        //biblioteca padrao para conexao com sqlserv
        //Gerenciar pacote de NuGet System.Data.SqlClient

        //biblioteca padrao para executar comando sql
        //Gerenciar pacote de NuGet Dapper(metodo que iremos utilizar) EntityFramework

        //metodo estatico para retornar string de conexao
        public static string GetConnectionString()
        {
            return @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=BdAgendaContatos;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}
