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
