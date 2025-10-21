using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task_Management.data;

namespace Task_Management
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ff.Text = "Please enter username and password!";
                return;
            }

            using (var db = new Context())
            {
                var user = db.task_user
                             .FirstOrDefault(u => u.Name_user == username && u.Password == password);

                if (user == null)
                {
                    ff.Text = "Invalid Username or Password!";
                    return;
                }

                if (user.Role == "Manager")
                {
                    new Manager().Show();
                }
                else
                {
                    new employeewindow(user).Show();
                }

                this.Close();
            }

        }
    }
}