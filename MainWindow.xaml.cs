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

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var todoItem = new CheckBox();
            todoItem.Content = taskInput.Text;
            todoItem.Width = 200;
            todoItem.Height = 24;
            tasksPanel.Children.Add(todoItem);
            taskInput.Clear();

        }

        private void taskInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.addTaskButton.IsEnabled = !string.IsNullOrWhiteSpace(taskInput.Text);
        }
    }
}

