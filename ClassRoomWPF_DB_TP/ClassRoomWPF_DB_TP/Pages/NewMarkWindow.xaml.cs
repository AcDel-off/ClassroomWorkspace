using ClassRoomWPF_DB_TP.Data;
using ClassRoomWPF_DB_TP.Models;
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

namespace ClassRoomWPF_DB_TP.Pages
{
    public partial class NewMarkWindow : Window
    {
        private int StudentId { get; }

        public NewMarkWindow(int studentId)
        {
            InitializeComponent();
            StudentId = studentId;
            StudentName.Text = $"Выставить оценку ученику: {new SchoolContext().Students.FirstOrDefault(s => s.Id == StudentId)!.FullName}";       
        }

        private void GiveGrade(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(GradeValue.Text, out int val) || val < 1 || val > 5)
            {
                MessageBox.Show("Некорректная оценка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using var db = new SchoolContext();
            var teacher = db.Teachers.Find(UserSession.CurrentUser.Id);
            var student = db.Students.SingleOrDefault(s => s.Id == StudentId);
            var subject = db.Subjects.Single(s => s.Name == teacher!.SubjectName);
            var grade = new Grade
            {
                Student = student!,
                Teacher = teacher!,
                Subject = subject,
                Value = val
            };
            db.Grades.Add(grade);
            db.SaveChanges();

            MessageBox.Show($"Оценка {val} выставлена {student!.FullName}");

            DialogResult = true;
            Close();
        }
    }
}
