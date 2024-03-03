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

            // Parcurge toate elementele din Content și le închide, cu excepția panoului asociat butonului apăsat
            foreach (UIElement element in (Content as Panel)?.Children)
            {
                if (element is FrameworkElement panel && panel.Name != panelName && panel.Name != "StackPanel" && (panel.Name == "AddWordPanel" || panel.Name == "DeleteWordPanel" || panel.Name == "UpdateWordPanel"))
                {
                    panel.Visibility = Visibility.Collapsed;
                }
                if (element is FrameworkElement panel1 && panel1.Name == panelName)
                {
                    panel1.Visibility = Visibility.Visible;

                }
            }
        }

    }
}