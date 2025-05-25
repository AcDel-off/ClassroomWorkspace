using ClassRoomWPF_DB_TP.Data;
using ClassRoomWPF_DB_TP.Models;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ClassRoomWPF_DB_TP.Pages
{
    public partial class ManageStudentsPage : Page
    {
        public ManageStudentsPage()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            using var db = new SchoolContext();
            StudentsList.ItemsSource = db.Students
                .Select(s => $"{s.FullName} ({s.Login})")
                .ToList();
        }

        private void AddStudent(object sender, RoutedEventArgs e)
        {
            var dlg = new NewStudentWindow();
            if (dlg.ShowDialog() == true)
                LoadStudents();
        }

        private void DeleteStudent(object sender, RoutedEventArgs e)
        {
            if (StudentsList.SelectedItem is string selected)
            {
                var parts = selected.Split(['(', ')'], StringSplitOptions.RemoveEmptyEntries);
                string fullName = parts[0].Trim();
                string login = parts[1].Trim();

                using var db = new SchoolContext();
                var student = db.Students
                    .Where(g => g.FullName == fullName && g.Login == login)
                    .FirstOrDefault();

                if (student != null)
                {
                    db.Students.Remove(student);
                    db.SaveChanges();
                    LoadStudents();
                }
                else
                {
                    MessageBox.Show($"Ученик не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
            => NavigationService?.Navigate(new AdministratorPage());
    }
}
