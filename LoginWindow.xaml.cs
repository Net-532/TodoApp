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
            String connenctionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=todoapp;Integrated Security=SSPI;";

            SqlConnection conn = new SqlConnection(connenctionString);

            try
            {
                conn.Open();
                // query
                SqlCommand dbCommand = new SqlCommand("SELECT COUNT(*) FROM dbo.users where username=@name and password=@pass", conn);
              //  dbCommand.CommandText = 
                dbCommand.Parameters.Add("@name", SqlDbType.VarChar);
                dbCommand.Parameters["@name"].Value = LoginTxt.Text;
                dbCommand.Parameters.Add("@pass", SqlDbType.VarChar);
                dbCommand.Parameters["@pass"].Value = PasswordTxt.Password;
                // dbCommand.CommandText = "SELECT COUNT(*) FROM dbo.users";
                // dbCommand.ExecuteNonQuery(); insert/delete/update
                int usercont = (int)dbCommand.ExecuteScalar();
                if(usercont == 0) {
                    throw new ApplicationException("User not found");
                }
                // dbCommand.ExecuteReader();
                Console.WriteLine("Connected");
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
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
