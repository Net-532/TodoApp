using System.Data.Common;
using System.Data.SqlClient;
using System;
using System.Windows;

namespace TodoApp
{
    internal class AuthenticationService
    {
        public void Authenticate(string login, string password)
        {
            string connenctionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=todoapp;Integrated Security=SSPI;";
            using (DbConnection conn = new SqlConnection(connenctionString))
            {
                DbCommand dbCommand = new SqlCommand();
                dbCommand.Connection = conn;
                dbCommand.CommandText = "SELECT COUNT(*) FROM dbo.users WHERE username=@name AND password=@pass";
                dbCommand.Parameters.Add(new SqlParameter("@name", login));
                dbCommand.Parameters.Add(new SqlParameter("@pass", password));
                var userCount = 0;
                try
                {
                    conn.Open();
                    userCount = (int)dbCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (userCount == 0)
                {
                    throw new AuthenticationException("Користувача не знайдено або пароль неправильний!");
                }
            }
        }

    }
}
