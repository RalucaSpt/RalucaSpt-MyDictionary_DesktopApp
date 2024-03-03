using Dictionary_App.MyApp;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
