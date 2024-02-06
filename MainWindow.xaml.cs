using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TodoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            bool allTasksCompleted = tasksPanel.Children.OfType<CheckBox>().All(c => c.IsChecked == true);

            if (allTasksCompleted)
            {
                MessageBox.Show("Ти - молодець! Всі завдання виконані, так тримати!");
            }
        }

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var todoItem = new CheckBox();
            todoItem.Content = taskInput.Text;
            todoItem.Width = 200;
            todoItem.Height = 24;
            todoItem.Checked += CheckBox_Checked;
            tasksPanel.Children.Add(todoItem);
            taskInput.Clear();

        }

        private void taskInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.addTaskButton.IsEnabled = !string.IsNullOrWhiteSpace(taskInput.Text);
        }
    }
}

