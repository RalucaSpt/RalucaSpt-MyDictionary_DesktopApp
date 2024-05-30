using Dictionary_App.MyApp;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Dictionary_App.Pages
{
    public partial class Authentification : Page
    {

        public Authentification()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Authenticate_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            if (Admins.admins.Any(admin => admin.username == username && admin.password == password))
            {
                this.NavigationService.Navigate(new Uri("Pages/Administration.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }
    }
}
