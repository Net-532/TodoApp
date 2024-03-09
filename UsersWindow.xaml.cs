using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace TodoApp
{
    public partial class UsersWindow : UserControl
    {
        private SqlConnection _dbConnection;

        private SqlDataAdapter _dataAdapter;

        private DataSet _dataSet;

        public UsersWindow()
        {
            InitializeComponent();
        }

        private void LoadUsers()
        {
            _dbConnection = new SqlConnection(DatabaseConnection.ConnenctionString);
            _dataAdapter = new SqlDataAdapter();
            _dataAdapter.SelectCommand = new SqlCommand("SELECT * FROM users WHERE username LIKE @username", _dbConnection);
            _dataAdapter.SelectCommand.Parameters.AddWithValue("@username", $"%{searchBox.Text}%");

            SqlCommand updateCommand = new SqlCommand("UPDATE users SET password=@password WHERE id=@id", _dbConnection);
            updateCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255, "username");
            updateCommand.Parameters.Add("@password", SqlDbType.Int, 5, "password");
            updateCommand.Parameters.Add("@id", SqlDbType.Int, 5, "id");
            _dataAdapter.UpdateCommand = updateCommand;

            SqlCommand insertCommand = new SqlCommand("INSERT INTO users (username, password) VALUES (@username, @password)", _dbConnection);
            insertCommand.Parameters.Add("@username", SqlDbType.NVarChar, 255, "username");
            insertCommand.Parameters.Add("@password", SqlDbType.NVarChar, 255, "password");
            _dataAdapter.InsertCommand = insertCommand;

            SqlCommand deleteCommand = new SqlCommand("DELETE FROM users WHERE id=@id", _dbConnection);
            deleteCommand.Parameters.Add("@id", SqlDbType.Int, 5, "id");
            _dataAdapter.DeleteCommand = deleteCommand;

            _dataSet = new DataSet("usersDs");
            _dataAdapter.Fill(_dataSet);
            UsersGrid.ItemsSource = _dataSet.Tables[0].DefaultView;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _dataAdapter.Update(_dataSet);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            FilterUserByUsernameFromDataView();
        }

        private void FilterUserByUsernameFromDataView()
        {
            _dataSet.Tables[0].DefaultView.RowFilter = $"username LIKE '%{searchBox.Text}%'";
        }

        private void FilterUserByUsernameFromDb()
        {
            _dataAdapter.SelectCommand.Parameters["@username"].Value = $"%{searchBox.Text}%";
            _dataSet.Clear();
            _dataAdapter.Fill(_dataSet);
        }
    }

}
