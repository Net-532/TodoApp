using System.Data.Common;
using System.Data.SqlClient;

namespace TodoApp
{
    internal class DatabaseConnection
    {
        private static DatabaseConnection? _instance;

        private readonly string _connenctionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=todoapp;Integrated Security=SSPI;";

        private DbConnection connection;

        private DatabaseConnection()
        {
            connection = new SqlConnection(_connenctionString);
        }

        public static DatabaseConnection GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DatabaseConnection();
            }
            return _instance;
        }

        public DbConnection GetConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }

    }
}
