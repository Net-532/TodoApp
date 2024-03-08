using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TodoApp
{
    public partial class LoginWindow : Window
    {
        LoginError loginError;

        private AuthenticationService authenticationService;

        public LoginWindow()
        {
            InitializeComponent();
            loginError = new LoginError();
            DataContext = loginError;
            authenticationService = new AuthenticationService();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                authenticationService.Authenticate(LoginTxt.Text, PasswordTxt.Password);
            }
            catch (AuthenticationException ex)
            {
                loginError.Message = ex.Message;
                return;
            }
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

    class LoginError: INotifyPropertyChanged
    {
 
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _message;

        public string Message
        {
            get { return _message; }
            set {
                _message = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Message"));
            }
        }

        public LoginError()
        {
        }
        
    }
}
