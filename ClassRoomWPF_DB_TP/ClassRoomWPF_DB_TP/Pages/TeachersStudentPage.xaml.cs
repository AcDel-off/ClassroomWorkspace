using ClassRoomWPF_DB_TP.Data;
using ClassRoomWPF_DB_TP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassRoomWPF_DB_TP.Pages
{
    public partial class TeachersStudentPage : Page
    {
        private readonly int studentId;

        public TeachersStudentPage(int studentId)
        {
            InitializeComponent();
            this.studentId = studentId;
            using var db = new SchoolContext();
            StudentName.Text = $"Оценки ученика: {db.Students.FirstOrDefault(s => s.Id == studentId)!.FullName}";            
            LoadGrades();
        }

        private void LoadGrades()
        {
            var teacher = UserSession.CurrentUser as Teacher;
            using var db = new SchoolContext();
            var grades = db.Grades
                .Where(g => g.StudentId == studentId && g.Subject.Name == teacher.SubjectName)
                .Select(g => $"{g.Subject.Name} - {g.Value} ({g.GivenAt:dd.MM.yyyy})")
                .ToList();

            GradesList.ItemsSource = grades;
        }

        private void ShowAverage(object sender, RoutedEventArgs e)
        {
            var teacher = UserSession.CurrentUser as Teacher;
            using var db = new SchoolContext();
            var avgs = db.Grades
                .Where(g => g.StudentId == studentId && g.Subject.Name == teacher.SubjectName)
                .Average(g => g.Value);

            MessageBox.Show($"{db.Students.FirstOrDefault(s => s.Id == studentId)!.FullName}: {avgs:F1}", "Средняя оценка по предмету");
        }

        private void ToNewMark(object sender, RoutedEventArgs e)
        {
            var dlg = new NewMarkWindow(studentId);
            if (dlg.ShowDialog() == true)
                LoadGrades();
        }

        private void ToUpdateMark(object sender, RoutedEventArgs e)
        {
            if (GradesList.SelectedItem is string selected)
            {
                using var db = new SchoolContext();
                var grade = GetGrade(selected);

                if (grade != null)
                {
                    var dlg = new UpdateMarkWindow(studentId, grade.Id);
                    if (dlg.ShowDialog() == true)
                        LoadGrades();
                }
                else
                {
                    MessageBox.Show($"Оценка не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteMark(object sender, RoutedEventArgs e)
        {
            if (GradesList.SelectedItem is string selected)
            {
                using var db = new SchoolContext();
                var grade = GetGrade(selected);

                if (grade != null)
                {
                    db.Grades.Remove(grade);
                    db.SaveChanges();
                    LoadGrades();
                }
                else
                {
                    MessageBox.Show($"Оценка не найдена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        Grade GetGrade(string selected)
        {
            var parts = selected.Split(['-', '(', ')'], StringSplitOptions.RemoveEmptyEntries);
            string subjectName = parts[0].Trim();
            int value = int.Parse(parts[1].Trim());
            DateTime givenAt = DateTime.ParseExact(parts[2].Trim(), "dd.MM.yyyy", null).ToLocalTime().ToUniversalTime();

            using var db = new SchoolContext();
            var grade = db.Grades
                .Where(g => g.StudentId == studentId && g.TeacherId == UserSession.CurrentUser.Id && g.Subject.Name == subjectName && g.Value == value && g.GivenAt.Day == givenAt.Day)
                .FirstOrDefault();

            return grade!;
        }

        private void GoBack(object sender, RoutedEventArgs e)
            => NavigationService?.Navigate(new TeacherPage());
    }
}
