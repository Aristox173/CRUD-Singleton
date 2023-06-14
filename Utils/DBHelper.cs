using System.Configuration;
using System.Data.SqlClient;

namespace CRUD.Utilities
{
    public static class DBHelper
    {
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        }

        public static void CloseSqlConnection(SqlConnection connection)
        {
            if (connection != null && connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
}