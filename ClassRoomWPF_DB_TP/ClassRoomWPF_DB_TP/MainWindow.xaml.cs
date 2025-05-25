using System.Windows;

namespace ClassRoomWPF_DB_TP;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new Pages.LoginPage());
    }
}