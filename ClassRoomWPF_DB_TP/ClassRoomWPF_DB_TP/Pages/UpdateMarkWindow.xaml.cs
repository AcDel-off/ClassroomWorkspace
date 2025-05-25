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
    public partial class UpdateMarkWindow : Window
    {
        private int StudentId { get; }
        private int GradeId { get; }

        public UpdateMarkWindow(int studentId, int gradeId)
        {
            InitializeComponent();
            StudentId = studentId;
            GradeId = gradeId;
            using var db = new SchoolContext();
            var grade = db.Grades.FirstOrDefault(s => s.Id == GradeId);
            StudentName.Text = $"Обновить оценку от {grade!.GivenAt:dd.MM.yyyy} ученика: {db.Students.FirstOrDefault(s => s.Id == StudentId)!.FullName}";
            GradeValue.Text = grade.Value.ToString();
        }

        private void UpdateGrade(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(GradeValue.Text, out int val) || val < 1 || val > 5)
            {
                MessageBox.Show("Некорректная оценка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using var db = new SchoolContext();
            var grade = db.Grades.FirstOrDefault(s => s.Id == GradeId);
            grade!.Value = val;
            db.SaveChanges();

            MessageBox.Show($"Оценка от {grade.GivenAt:dd.MM.yyyy} обновлена: {db.Students.FirstOrDefault(s => s.Id == StudentId)!.FullName}");

            DialogResult = true;
            Close();
        }
    }
}
