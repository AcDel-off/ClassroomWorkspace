using ClassRoomWPF_DB_TP.Data;
using ClassRoomWPF_DB_TP.Models;
using System.Windows;
using System.Windows.Controls;

namespace ClassRoomWPF_DB_TP.Pages
{
    public partial class TeacherPage : Page
    {
        public TeacherPage() => InitializeComponent();

        private void FindStudent(object sender, RoutedEventArgs e)
        {
            using var db = new SchoolContext();
            var student = db.Students.SingleOrDefault(s => s.Login == StudentLogin.Text) ?? db.Students.SingleOrDefault(s => s.FullName == StudentLogin.Text);
            if (student == null)
            {
                MessageBox.Show("Ученик не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                NavigationService?.Navigate(new TeachersStudentPage(student.Id));
            }
        }

        private void Logout(object sender, RoutedEventArgs e)
            => NavigationService?.Navigate(new LoginPage());
    }
}
