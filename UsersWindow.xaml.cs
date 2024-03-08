using Microsoft.VisualBasic.Logging;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Input;

namespace TodoApp
{
    public partial class UsersWindow : UserControl
    {
        private SqlDataAdapter _dataAdapter;

        private DataSet _dataSet;

        public UsersWindow()
        {
            InitializeComponent();
        }

        private void LoadUsers()
        {
            SqlConnection dbConnection = new SqlConnection(DatabaseConnection.ConnenctionString);
            _dataAdapter = new SqlDataAdapter("SELECT * FROM users", dbConnection);
            SqlCommand updateCommand = new SqlCommand("UPDATE users SET password=@password WHERE id=@id", dbConnection);
            updateCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255, "username");
            updateCommand.Parameters.Add("@password", SqlDbType.Int, 5, "password");
            updateCommand.Parameters.Add("@id", SqlDbType.Int, 5, "id");
            _dataAdapter.UpdateCommand = updateCommand;

            SqlCommand insertCommand = new SqlCommand("INSERT INTO users (username, password) VALUES (@username, @password)", dbConnection);
            insertCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255, "username");
            insertCommand.Parameters.Add("@password", SqlDbType.NVarChar, 255, "password");
            _dataAdapter.InsertCommand = insertCommand;

            SqlCommand deleteCommand = new SqlCommand("DELETE FROM users WHERE id=@id", dbConnection);
            deleteCommand.Parameters.Add("@id", SqlDbType.Int, 5, "id");
            _dataAdapter.DeleteCommand = deleteCommand;

            _dataSet = new DataSet("usersDs");
            _dataAdapter.Fill(_dataSet);
            UsersGrid.ItemsSource = _dataSet.Tables[0].DefaultView;
        }

        private void WindowLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _dataAdapter.Update(_dataSet);
        }
    }

}
