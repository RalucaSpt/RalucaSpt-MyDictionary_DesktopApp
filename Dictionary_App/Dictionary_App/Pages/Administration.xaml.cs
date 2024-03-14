using Dictionary_App.MyApp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace Dictionary_App.Pages
{
    public partial class Administration : Page
    {
        Word word = new Word();
        List<Word> words = new List<Word>();
        List<string> categories;
        public Administration()
        {
            InitializeComponent();
            // Update the categories list
            if (File.Exists("../../Json/category.json"))
            {
                categories = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("../../Json/category.json"));
                AddItems_ComboBox(ExistingCategoryComboBox);
            }

            // Update the words list
            if (File.Exists("../../Json/word.json"))
            {
                words = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("../../Json/word.json"));
                AddItems_ComboBox(WordComboBox, words); // Umple ComboBox-ul cu toate cuvintele
            }

            // Set the data context
            this.DataContext = word;
        }
        private void AddItems_ComboBox(ComboBox comboBoxName)
        {
            //goleste combobox-ul
            comboBoxName.Items.Clear();
            if (categories == null)
            {
                comboBoxName.Items.Add("No categories");
                return;
            }
            // Add the categories to the combobox
            foreach (string category in categories)
            {
                comboBoxName.Items.Add(category);
            }
            // Set the selected index to the first item
            if (categories.Count > 0)
            {
                comboBoxName.SelectedIndex = 0;
            }
        }

        private void ADD_Click(object sender, RoutedEventArgs e)
        {
            AddWordPanel.Visibility = Visibility.Visible;
            DeleteWordPanel.Visibility = Visibility.Collapsed;
            UpdateWordPanel.Visibility = Visibility.Collapsed;
        }

        private void DELETE_Click(object sender, RoutedEventArgs e)
        {
            AddWordPanel.Visibility = Visibility.Collapsed;
            DeleteWordPanel.Visibility = Visibility.Visible;
            UpdateWordPanel.Visibility = Visibility.Collapsed;
        }

        private void UPDATE_Click(object sender, RoutedEventArgs e)
        {
            AddWordPanel.Visibility = Visibility.Collapsed;
            DeleteWordPanel.Visibility = Visibility.Collapsed;
            UpdateWordPanel.Visibility = Visibility.Visible;

            //initialize the combobox
            AddItems_ComboBox(UpdateExistingCategoryComboBox);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string json;
            // Verify if the image path is null
            if (ExistingCategoryComboBox.Visibility == Visibility.Visible)
            {
                word._category = ExistingCategoryComboBox.Text;
            }
            else
            {
                //check if the category already exists
                if (categories != null && categories.Contains(NewCategoryText.Text))
                {
                    MessageBox.Show("This category already exists!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    word._category = NewCategoryText.Text;
                    categories.Add(word._category);
                    json = JsonConvert.SerializeObject(categories, Formatting.Indented);
                    File.WriteAllText("..\\..\\Json\\category.json", json + Environment.NewLine);
                }
            }
            // Verify if the image path is null
            if (word._imagePath == null)
            {
                word._imagePath = "..\\Images\\no_image.jpg";
            }

            // Crează o listă de obiecte Word din conținutul fișierului JSON
            words = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("..\\..\\Json\\word.json"));

            // Adaugă noul cuvânt la lista de cuvinte
            Word newWord = new Word
            {
                _name = WordToAddTextBox.Text,
                _definition = DefinitionToAddTextBox.Text,
                _imagePath = ImageToAddTextBox.Text,
                _category = ExistingCategoryComboBox.Visibility == Visibility.Visible ? ExistingCategoryComboBox.Text : NewCategoryText.Text
            };
            words.Add(newWord);

            // Serializează lista actualizată de cuvinte și o scrie în fișierul JSON
            json = JsonConvert.SerializeObject(words, Formatting.Indented);
            File.WriteAllText("..\\..\\Json\\word.json", json + Environment.NewLine);
            MessageBox.Show("Data been inserted!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);

            // Reset the textboxes
            WordToAddTextBox.Text = "";
            DefinitionToAddTextBox.Text = "";
            ImageToAddTextBox.Text = "";
            NewCategoryText.Text = "";

            // Refresh the categories list
            categories = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("..\\..\\Json\\category.json"));
            AddItems_ComboBox(ExistingCategoryComboBox);
        }

        private void NewCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            NewCategoryButton.Visibility = Visibility.Collapsed;
            ExistingCategoryComboBox.Visibility = Visibility.Collapsed;
            ExistingCategoryButton.Visibility = Visibility.Visible;
            NewCategoryText.Visibility = Visibility.Visible;     
            
            UpdateNewCategoryButton.Visibility = Visibility.Collapsed;
            UpdateExistingCategoryComboBox.Visibility = Visibility.Collapsed;
            UpdateExistingCategoryButton.Visibility = Visibility.Visible;
            NewCategoryTextBox.Visibility = Visibility.Visible;

            if (word._category != null)
            {
                word._category.Remove(0);
            }
        }

        private void ExistingCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            // Change the visibility of the buttons and textboxes
            ExistingCategoryButton.Visibility = Visibility.Collapsed;
            NewCategoryText.Visibility = Visibility.Collapsed;
            ExistingCategoryComboBox.Visibility = Visibility.Visible;
            NewCategoryButton.Visibility = Visibility.Visible;

            UpdateExistingCategoryButton.Visibility = Visibility.Collapsed;
            NewCategoryTextBox.Visibility = Visibility.Collapsed;
            UpdateExistingCategoryComboBox.Visibility = Visibility.Visible;
            UpdateNewCategoryButton.Visibility = Visibility.Visible;
            if (word._category != null)
            {
                word._category.Remove(0);
            }
        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a file dialog to select an image
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Fișiere de imagine|*.jpg;*.jpeg;*.png;*.gif|Toate fișierele|*.*";
            bool? result = openFileDialog.ShowDialog();

            // If the user selects an image, copy it to the Images folder and update the image path
            if (result == true)
            {
                try
                {
                    // Copy the image to the Images folder
                    string newImagePath = openFileDialog.FileName;
                    string destinationPath = System.IO.Path.Combine("..\\..\\Images", System.IO.Path.GetFileName(newImagePath));
                    System.IO.File.Copy(newImagePath, destinationPath, true);

                    // Update the image path
                    ImageToAddTextBox.Text = destinationPath;
                    NewImageTextBox.Text = destinationPath;
                    word._imagePath = destinationPath;
                }
                catch (Exception ex)
                {
                    // Display an error message if the file can't be accessed
                    MessageBox.Show($"File can't be accessed!: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            //delete the word from the JSON file
            string wordToDelete = WordToDeleteTextBox.Text;

            words = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("..\\..\\Json\\word.json"));
            bool found = false;
            for (int i = words.Count - 1; i >= 0; i--)
            {
                Word w = words[i];
                if (w._name == wordToDelete)
                {
                    words.RemoveAt(i);
                    found = true;
                }
            }

            if (found)
            {
                string json = JsonConvert.SerializeObject(words, Formatting.Indented);
                File.WriteAllText("..\\..\\Json\\word.json", json);
                MessageBox.Show("Data been deleted!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("The word does not exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            WordToDeleteTextBox.Text = "";
        }

        private void AddItems_ComboBox(ComboBox comboBoxName, List<Word> wordList)
        {
            //goleste combobox-ul
            comboBoxName.Items.Clear();

            // Add the words to the combobox
            foreach (Word word in wordList)
            {
                comboBoxName.Items.Add(word._name);
            }

            // Set the selected index to the first item
            if (wordList.Count > 0)
            {
                comboBoxName.SelectedIndex = 0;
            }
        }

        // Metoda care reacționează la selecția unui cuvânt în ComboBox
        private void WordComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifică dacă un cuvânt a fost selectat
            if (WordComboBox.SelectedItem != null)
            {
                // Găsește cuvântul selectat în lista de cuvinte
                string selectedWord = WordComboBox.SelectedItem.ToString();
                Word wordToUpdate = words.Find(w => w._name == selectedWord);

                // Verifică dacă cuvântul a fost găsit
                if (wordToUpdate != null)
                {
                    // Afișează datele cuvântului selectat în câmpurile de text corespunzătoare
                    NewDefinitionTextBox.Text = wordToUpdate._definition;
                    NewImageTextBox.Text = wordToUpdate._imagePath;
                    NewCategoryTextBox.Text = wordToUpdate._category;
                    UpdateExistingCategoryComboBox.SelectedItem = wordToUpdate._category;
                }
                else
                {
                    MessageBox.Show("The selected word does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        // În metoda UpdateButton_Click pentru a actualiza un cuvânt
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Verifică dacă un cuvânt a fost selectat pentru actualizare
            if (string.IsNullOrEmpty(WordComboBox.Text))
            {
                MessageBox.Show("Please select a word to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Găsește cuvântul care trebuie actualizat
            Word wordToUpdate = words.Find(w => w._name == WordComboBox.Text);
            if (wordToUpdate == null)
            {
                MessageBox.Show("The selected word does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Actualizează informațiile cu noile valori
            words.Remove(wordToUpdate);
            wordToUpdate._definition = NewDefinitionTextBox.Text;
            wordToUpdate._imagePath = NewImageTextBox.Text;
            if (UpdateExistingCategoryComboBox.Visibility == Visibility.Visible)
            {
                wordToUpdate._category = UpdateExistingCategoryComboBox.Text;
            }
            else
            {
                //check if the category already exists
                if (categories != null && categories.Contains(NewCategoryTextBox.Text))
                {
                    MessageBox.Show("This category already exists!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    wordToUpdate._category = NewCategoryTextBox.Text;

                    categories.Add(wordToUpdate._category);
                    string json_c = JsonConvert.SerializeObject(categories, Formatting.Indented);
                    File.WriteAllText("..\\..\\Json\\category.json", json_c + Environment.NewLine);
                }
            }
            words.Add(wordToUpdate);

            // Serializare lista actualizată de cuvinte și actualizare fișier JSON
            string json = JsonConvert.SerializeObject(words, Formatting.Indented);
            File.WriteAllText("..\\..\\Json\\word.json", json);

            MessageBox.Show("Word updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Resetarea câmpurilor de text
            WordComboBox.Text = "";
            NewDefinitionTextBox.Text = "";
            NewImageTextBox.Text = "";
            NewCategoryTextBox.Text = "";
            UpdateExistingCategoryComboBox.Text = "";
        }
    }
}