using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TodoApp
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string connenctionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=todoapp;Integrated Security=SSPI;";

            DbConnection conn = new SqlConnection(connenctionString);
            try
            {
                conn.Open();
                DbCommand dbCommand = new SqlCommand();
                dbCommand.Connection = conn;
                dbCommand.CommandText = "SELECT COUNT(*) FROM dbo.users WHERE username=@name AND password=@pass";
                dbCommand.Parameters.Add(new SqlParameter("@name", LoginTxt.Text));
                dbCommand.Parameters.Add(new SqlParameter("@pass", PasswordTxt.Password));
                var userCount = (int)dbCommand.ExecuteScalar();
                if (userCount == 0)
                {
                    MessageBox.Show("User not found");
                }
                else
                {
                    MessageBox.Show("User is not found");
                }

                MessageBox.Show("Connected");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

            new MainWindow().Show();
            Close();
        }

        private void LoginPlaceholder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // оскільки плейсхолдер розташований поверх текстового поля за допомогою властивості Panel.ZIndex="1"
            // то перехватуємо клік мишки на плейсхолдері та фокусуємо текстове поле
            LoginTxt.Focus();
        }

        private void LoginTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            var loginTextIsEmpty = string.IsNullOrWhiteSpace(LoginTxt.Text);
            // Якщо текст не введено
            if (loginTextIsEmpty)
            {
                // показуємо плейсхолдер "Enter username"
                this.LoginPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                // скриваємо плейсхолдер "Enter username"
                this.LoginPlaceholder.Visibility = Visibility.Hidden;
            }
        }
    }

}
