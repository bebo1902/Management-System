using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Task_Management.data;
using Task_Management.models;

namespace Task_Management
{

    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        Context con = new Context();
        public Manager()
        {
            InitializeComponent();
            loud();
        }
        private void loud()
        {
            using (var db = new Context())
            {
                var user = (from n in db.tasks
                            join e in db.task_user on n.id_user equals e.UserID
                            select new
                            {
                                TaskId = n.TaskID,
                                Title = n.Title,
                                EmployeeName = e.Name_user,
                                Description = n.Description,
                                Status = n.Status,
                            }).ToList();
                ManagerTasksDataGrid.ItemsSource = user;

            }



            //{
            //    using (var db = new Context())
            //    {
            //        var items = (from t in db.tasks
            //                     join u in db.task_user
            //                     on t.id_user equals u.UserID
            //                     select new
            //                     {
            //                         TaskID = t.TaskID,
            //                         Title = t.Title,
            //                         Description = t.Description,
            //                         Status = t.Status,
            //                         EmployeeName = u.Name_user
            //                     }).ToList();

            //        ManagerTasksDataGrid.ItemsSource = items;
            //    }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tit.Text) ||
                string.IsNullOrWhiteSpace(desc.Text) ||
                comb.SelectedItem == null ||
                string.IsNullOrWhiteSpace(emp.Text))
            {
                MessageBox.Show("Please fill all fields!");
                return;
            }

            using (var con = new Context())
            {
                string title = tit.Text;
                string description = desc.Text;
                string status = (comb.SelectedItem as ComboBoxItem).Content.ToString();
                string employeeName = emp.Text;
                int id = int.Parse(idt.Text);

                // تحقق أن الموظف موجود بالفعل
                var existingUser = con.task_user.FirstOrDefault(u => u.Name_user == employeeName);
                if (existingUser == null)
                {
                    MessageBox.Show("Employee not found! Please enter a valid existing user.");
                    return;
                }

                var newTask = new Tasks
                {
                    Title = title,
                    Description = description,
                    Status = status,
                    id_user = existingUser.UserID,
                    TaskID = id

                };

                con.tasks.Add(newTask);
                con.SaveChanges();

                MessageBox.Show("Task added successfully!");
                loud(); // إعادة تحميل البيانات
            }
        }

        private void ManagerTasksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ManagerTasksDataGrid.SelectedItem;
            if (selected != null)
            {
                dynamic  row = selected;
                idt.Text = row.TaskId.ToString();
                tit.Text = row.Title;
                desc.Text = row.Description;
                emp.Text = row.EmployeeName;
                comb.Text = row.Status;
            }
        }
       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(idt.Text, out int id))
            {
                var task = con.tasks.FirstOrDefault(t => t.TaskID == id);

                if (task != null)
                {
                    task.Title = tit.Text;
                    task.Description = desc.Text;
                    task.Status = (comb.SelectedItem as ComboBoxItem).Content.ToString();

                   
                    var user = con.task_user.FirstOrDefault(u => u.UserID == task.id_user);
                    if (user != null)
                    {
                        user.Name_user = emp.Text;
                    }

                    con.SaveChanges();
                    MessageBox.Show("Updated Successfully!");
                    loud(); 
                }
            }
            else
            {
                MessageBox.Show("Invalid Task ID");
            }
        }
      

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (ManagerTasksDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select a task to delete.");
                return;
            }

            dynamic selected = ManagerTasksDataGrid.SelectedItem;
            int taskId = selected.TaskId;

            var taskToDelete = con.tasks.FirstOrDefault(t => t.TaskID == taskId);
            if (taskToDelete != null)
            {
                con.tasks.Remove(taskToDelete);
                con.SaveChanges();

                MessageBox.Show("Task deleted successfully!");
                loud();
            }
            else
            {
                MessageBox.Show("Task not found!");
            }
            idt.Text = " ";
            tit.Text = " ";
            desc.Text = " ";
            emp.Text = " ";
           
            }
    }
}
