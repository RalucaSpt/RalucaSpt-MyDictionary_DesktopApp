using Dictionary_App.MyApp;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace Dictionary_App.Pages
{

    public partial class SearchWord : Page
    {
        public SearchWord()
        {
            InitializeComponent();
            InitializeComboBox();

        }

        private void InitializeComboBox()
        {
            categoryComboBox.ItemsSource = MyDictionary._categories;
        }

        private void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCategory = categoryComboBox.SelectedItem.ToString();
            if (selectedCategory != null && selectedCategory != "All")
            {
                MyDictionary.GetSuggestedWords(selectedCategory);


                if (MyDictionary._suggestedWords.Any())
                {
                    suggestionsListBox.Items.Clear();
                    foreach (var word in MyDictionary._suggestedWords)
                    {
                        suggestionsListBox.Items.Add(word._name);
                    }
                }
                else
                {
                    txtSearch.Text.Remove(0);
                }
            }
            suggestionsListBox.Visibility = Visibility.Visible;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            suggestionsListBox.Visibility = Visibility.Visible;
            string searchText = txtSearch.Text.ToLower();
            var filteredWords = MyDictionary._suggestedWords.Where(word => word._name.ToLower().StartsWith(searchText)).ToList();
            suggestionsListBox.Items.Clear();
            foreach (var word in filteredWords)
            {
                suggestionsListBox.Items.Add(word._name);
            }
        }

        private void suggestionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            suggestionsListBox.Visibility = Visibility.Collapsed;
            if (suggestionsListBox.SelectedItem != null)
                MyDictionary._selectedWord = MyDictionary._suggestedWords.Where(word => word._name == suggestionsListBox.SelectedItem.ToString()).FirstOrDefault();
            BitmapImage bitmap = new BitmapImage(new Uri(MyDictionary._selectedWord._imagePath, UriKind.Relative));
            imgWord.Source = bitmap;
            txtDescription.Text = MyDictionary._selectedWord._definition;
            txtName.Text = MyDictionary._selectedWord._name;
            txtSearch.Text = MyDictionary._selectedWord._name;
        }
    }
}
