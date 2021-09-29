using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenti.DAL.DALSQL
{
   public abstract class BaseDALSQL
    {


        public static SqlConnection GetConnection()
        {
            var connectionString =ConfigurationManager.ConnectionStrings ["StudentsConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }


    }
}
