using ClassRoomWPF_DB_TP.Data;
using ClassRoomWPF_DB_TP.Models;
using System.Windows;
using System.Windows.Controls;

namespace ClassRoomWPF_DB_TP.Pages
{
    public partial class NewStudentWindow : Window
    {
        public NewStudentWindow() => InitializeComponent();

        private void CreateStudent(object sender, RoutedEventArgs e)
        {
            using var db = new SchoolContext();
            var st = new Student
            {
                FullName = FullNameBox.Text,
                Login = LoginBox.Text,
                Password = PasswordBox.Password
            };
            db.Students.Add(st);
            db.SaveChanges();

            DialogResult = true;
            Close();
        }
    }
}
