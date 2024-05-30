using System;
using System.Windows;
using System.Windows.Controls;

namespace Dictionary_App.Pages
{
    public partial class Landing : Page
    {
        public Landing()
        {
            InitializeComponent();
        }

        private void DictionaryBut_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/SearchWord.xaml", UriKind.Relative));
        }

        private void GameBut_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/Game.xaml", UriKind.Relative));
        }

        private void AdminBut_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/Authentification.xaml", UriKind.Relative));
        }
    }
}

