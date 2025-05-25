using ClassRoomWPF_DB_TP.Data;
using ClassRoomWPF_DB_TP.Models;
using System.Windows;
using System.Windows.Controls;

namespace ClassRoomWPF_DB_TP.Pages
{
    public partial class NewTeacherWindow : Window
    {
        public NewTeacherWindow() => InitializeComponent();

        private void CreateTeacher(object sender, RoutedEventArgs e)
        {
            using var db = new SchoolContext();
            var subject = db.Subjects.FirstOrDefault(s => s.Name == SubjectBox.Text)
                       ?? new Subject { Name = SubjectBox.Text };
            if (subject.Id == 0) db.Subjects.Add(subject);

            var teacher = new Teacher
            {
                FullName = FullNameBox.Text,
                Login = LoginBox.Text,
                Password = PasswordBox.Password,
                SubjectName = subject.Name
            };
            db.Teachers.Add(teacher);
            db.SaveChanges();

            DialogResult = true;
            Close();
        }
    }
}
