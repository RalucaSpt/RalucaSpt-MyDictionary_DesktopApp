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
    /// <summary>
    /// Interaction logic for Administration.xaml
    /// </summary>
    public partial class Administration : Page
    {
        public Administration()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddWordPanel.Visibility = Visibility.Visible;
            // Ascunde containerul pentru ștergere cuvânt (dacă este vizibil)
            DeleteWordPanel.Visibility = Visibility.Collapsed;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Schimbă vizibilitatea containerului pentru ștergere cuvânt
            DeleteWordPanel.Visibility = Visibility.Visible;
            // Ascunde containerul pentru adăugare cuvânt (dacă este vizibil)
            AddWordPanel.Visibility = Visibility.Collapsed;
        }

        private void TogglePanelVisibility(object sender, RoutedEventArgs e)
        {
            // Obține butonul care a fost apăsat
            Button button = (Button)sender;

            // Obține numele containerului asociat butonului din atributul Tag
            string panelName = button.Tag.ToString();

            // Găsește containerul folosind numele
            FrameworkElement panel = FindName(panelName) as FrameworkElement;

            // Inversează vizibilitatea containerului, dacă este găsit
            if (panel != null)
            {
                panel.Visibility = panel.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}