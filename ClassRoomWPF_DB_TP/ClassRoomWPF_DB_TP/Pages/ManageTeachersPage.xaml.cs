using ClassRoomWPF_DB_TP.Data;
using System.Windows;
using System.Windows.Controls;

namespace ClassRoomWPF_DB_TP.Pages
{
    public partial class ManageTeachersPage : Page
    {
        public ManageTeachersPage()
        {
            InitializeComponent();
            LoadTeachers();
        }

        private void LoadTeachers()
        {
            using var db = new SchoolContext();
            TeachersList.ItemsSource = db.Teachers
                //.Select(t => $"{t.Id}: {t.FullName} ({t.Login}) – {t.SubjectName}")
                .Select(t => $"{t.FullName} ({t.Login}) - {t.SubjectName}")
                .ToList();
        }

        private void AddTeacher(object sender, RoutedEventArgs e)
        {
            var dlg = new NewTeacherWindow();
            if (dlg.ShowDialog() == true)
                LoadTeachers();
        }

        private void DeleteTeacher(object sender, RoutedEventArgs e)
        {
            if (TeachersList.SelectedItem is string selected)
            {
                var parts = selected.Split(['(', ')', '-'], StringSplitOptions.RemoveEmptyEntries);
                string fullName = parts[0].Trim();
                string login = parts[1].Trim();
                string subject = parts.Last().Trim();

                using var db = new SchoolContext();
                var teacher = db.Teachers
                    .Where(g => g.FullName == fullName && g.Login == login && g.SubjectName == subject)
                    .FirstOrDefault();

                if (teacher != null)
                {
                    db.Teachers.Remove(teacher);
                    db.SaveChanges();
                    LoadTeachers();
                }
                else
                {
                    MessageBox.Show($"Учитель не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
            => NavigationService?.Navigate(new AdministratorPage());
    }
}
