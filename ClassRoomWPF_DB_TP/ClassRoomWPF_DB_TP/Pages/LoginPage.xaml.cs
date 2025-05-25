using ClassRoomWPF_DB_TP.Data;
using ClassRoomWPF_DB_TP.Models;
using System.Windows;
using System.Windows.Controls;

namespace ClassRoomWPF_DB_TP.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage() => InitializeComponent();

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text;
            string pwd = PasswordBox.Password;

            using var db = new SchoolContext();

            var user = db.Persons.FirstOrDefault(p => p.Login == login && p.Password == pwd);
            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            UserSession.CurrentUser = user;
            NavigateToHome(user);
        }

        private void NavigateToHome(Person user)
        {
            Page target = user switch
            {
                Student => new StudentPage(),
                Teacher => new TeacherPage(),
                Administrator => new AdministratorPage(),
                _ => throw new Exception("Неизвестная роль")
            };

            NavigationService?.Navigate(target);
        }
    }
}
