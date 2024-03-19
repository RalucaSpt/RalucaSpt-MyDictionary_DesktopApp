using Dictionary_App.MyApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    public partial class Game : Page
    {
        public Game()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            GameLogic._currentRound = 1;
            GameLogic._score = 0;
            ShowNextWord();
        }

        private void ShowNextWord()
        {
            if (GameLogic._currentRound <= 5)
            {
                if (GameLogic._wordsToGuess[GameLogic._currentRound - 1]._imagePath == "..\\Images\\no_image.jpg")
                {
                    txtDescription.Visibility = Visibility.Visible;
                    border.Visibility = Visibility.Visible;
                    imgWord.Visibility = Visibility.Hidden;
                    txtDescription.Text = GameLogic._wordsToGuess[GameLogic._currentRound - 1]._definition;
                }
                else
                {
                    imgWord.Visibility = Visibility.Visible;
                    border.Visibility = Visibility.Hidden;
                    txtDescription.Visibility = Visibility.Hidden;
                    imgWord.Source = new BitmapImage(new Uri(GameLogic._wordsToGuess[GameLogic._currentRound - 1]._imagePath, UriKind.Relative));
                }
                countRounds.Text = $"Word {GameLogic._currentRound}/5";
                txtGuess.Text = "";
            }
            else
            {
                txtDescription.Visibility = Visibility.Visible;
                txtGuess.Visibility = Visibility.Hidden;
                countRounds.Visibility = Visibility.Hidden;
                imgWord.Visibility = Visibility.Hidden;
                btnExit.Visibility = Visibility.Visible;
                txtDescription.Text = $"Your score is {GameLogic._score}/5";
            }
        }

        private void CheckGuess()
        {
            if (txtGuess.Text.Equals(GameLogic._wordsToGuess[GameLogic._currentRound - 1]._name, StringComparison.OrdinalIgnoreCase))
            {
                GameLogic._score++;
                // Display a message that the guess is correct

            }
            GameLogic._currentRound++;
            ShowNextWord();
        }

        private void txtGuess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CheckGuess();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/Landing.xaml", UriKind.Relative));
        }
    }

}
