using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Task_Management.data;
using Task_Management.models;

namespace Task_Management
{
    public partial class employeewindow : Window
    {
        private Task_User currentUser;

        public employeewindow(Task_User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadTasks();
        }

        ObservableCollection<Tasks> pendingTasks;
        ObservableCollection<Tasks> completedTasks;

        private void LoadTasks()
        {
            using (var db = new Context())
            {
                pendingTasks = new ObservableCollection<Tasks>(
                    db.tasks
                      .Where(t => t.User.UserID == currentUser.UserID && t.Status != "Completed")
                      .ToList()
                );
                TasksDataGrid.ItemsSource = pendingTasks;

                completedTasks = new ObservableCollection<Tasks>(
                    db.tasks
                      .Where(t => t.User.UserID == currentUser.UserID && t.Status == "Completed")
                      .ToList()
                );
                CompletedTasksDataGrid.ItemsSource = completedTasks;
            }
        }

        

        private void SaveButton_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedTask = TasksDataGrid.SelectedItem as Tasks;
            if (selectedTask == null || StatusComboBox.SelectedItem == null) return;

            string newStatus = (StatusComboBox.SelectedItem as ComboBoxItem).Content.ToString();

            using (var db = new Context())
            {
                var taskInDb = db.tasks.FirstOrDefault(t => t.TaskID == selectedTask.TaskID);
                if (taskInDb != null)
                {
                    taskInDb.Status = newStatus;
                    db.SaveChanges();
                }
            }

           
            selectedTask.Status = newStatus;

           
            if (newStatus == "Completed")
            {
                pendingTasks.Remove(selectedTask);
                completedTasks.Add(selectedTask);
            }

          
            StatusComboBox.SelectedIndex = -1;
            TasksDataGrid.SelectedItem = null;
        }
    }
}