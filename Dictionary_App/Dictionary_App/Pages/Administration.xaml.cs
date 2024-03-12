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
            List<Word> wordList = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("..\\..\\Json\\word.json"));

            // Adaugă noul cuvânt la lista de cuvinte
            Word newWord = new Word
            {
                _name = WordToAddTextBox.Text,
                _definition = DefinitionToAddTextBox.Text,
                _imagePath = ImageToAddTextBox.Text,
                _category = ExistingCategoryComboBox.Visibility == Visibility.Visible ? ExistingCategoryComboBox.Text : NewCategoryText.Text
            };
            wordList.Add(newWord);

            // Serializează lista actualizată de cuvinte și o scrie în fișierul JSON
            json = JsonConvert.SerializeObject(wordList, Formatting.Indented);
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

            List<Word> wordList = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("..\\..\\Json\\word.json"));
            bool found = false;
            for (int i = wordList.Count - 1; i >= 0; i--)
            {
                Word w = wordList[i];
                if (w._name == wordToDelete)
                {
                    wordList.RemoveAt(i);
                    found = true;
                }
            }

            if (found)
            {
                string json = JsonConvert.SerializeObject(wordList, Formatting.Indented);
                File.WriteAllText("..\\..\\Json\\word.json", json);
                MessageBox.Show("Data been deleted!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("The word does not exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            WordToDeleteTextBox.Text = "";
        }
    }
}