using System.Windows;
using System.Windows.Controls;

namespace ClassRoomWPF_DB_TP.Pages
{
    public partial class AdministratorPage : Page
    {
        public AdministratorPage() => InitializeComponent();

        private void ManageStudents(object sender, RoutedEventArgs e)
            => NavigationService?.Navigate(new ManageStudentsPage());

        private void ManageTeachers(object sender, RoutedEventArgs e)
            => NavigationService?.Navigate(new ManageTeachersPage());

        private void Logout(object sender, RoutedEventArgs e)
            => NavigationService?.Navigate(new LoginPage());
    }
}
