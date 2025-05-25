using ClassRoomWPF_DB_TP.Data;
using System.Windows;
using System.Windows.Controls;

namespace ClassRoomWPF_DB_TP.Pages
{
    public partial class StudentPage : Page
    {
        public StudentPage()
        {
            InitializeComponent();
            LoadGrades();
        }

        private void LoadGrades()
        {
            using var db = new SchoolContext();
            var student = db.Students
                .Where(s => s.Id == UserSession.CurrentUser.Id)
                .SelectMany(s => s.Grades)
                .Select(g => $"{g.Subject.Name}: {g.Value} ({g.GivenAt:dd.MM.yyyy})")
                .ToList();

            GradesList.ItemsSource = student;
        }

        private void ShowAverages(object sender, RoutedEventArgs e)
        {
            using var db = new SchoolContext();
            var avgs = db.Grades
                .Where(g => g.StudentId == UserSession.CurrentUser.Id)
                .GroupBy(g => g.Subject.Name)
                .Select(gr => $"{gr.Key}: {gr.Average(g => g.Value):F1}")
                .ToList();

            MessageBox.Show(string.Join("\n", avgs), "Средние оценки");
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new LoginPage());
        }
    }
}
