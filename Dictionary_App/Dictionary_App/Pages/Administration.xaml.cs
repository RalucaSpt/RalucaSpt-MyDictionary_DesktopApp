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

        public Administration()
        {
            InitializeComponent();
            AddCategory_ComboBox(ExistingCategoryComboBox);
            AddWords_ComboBox(WordComboBox);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (ExistingCategoryComboBox.Visibility == Visibility.Visible)
            {
                MyDictionary._selectedWord._category = ExistingCategoryComboBox.Text;
            }
            else
            {
                if (MyDictionary._categories != null && MyDictionary._categories.Contains(NewCategoryText.Text))
                {
                    MessageBox.Show("This category already exists!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    MyDictionary._selectedWord._category = NewCategoryText.Text;
                    MyDictionary.AddCategory(NewCategoryText.Text);
                    ExistingCategoryComboBox.Items.Add(NewCategoryText.Text);
                }
            }

            MyDictionary.VerifyImageExists();

            Word newWord = new Word
            {
                _name = WordToAddTextBox.Text,
                _definition = DefinitionToAddTextBox.Text,
                _imagePath = ImageToAddTextBox.Text,
                _category = ExistingCategoryComboBox.Visibility == Visibility.Visible ? ExistingCategoryComboBox.Text : NewCategoryText.Text
            };
            MyDictionary.AddWord(newWord);

            WordToAddTextBox.Text = "";
            DefinitionToAddTextBox.Text = "";
            ImageToAddTextBox.Text = "";
            NewCategoryText.Text = "";

        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MyDictionary.DeleteWord(WordToDeleteTextBox.Text);
            WordToDeleteTextBox.Text.Remove(0);
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(WordComboBox.Text))
            {
                MessageBox.Show("Please select a word to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MyDictionary._selectedWord = null;
            MyDictionary._selectedWord = MyDictionary._words.Find(w => w._name == WordComboBox.Text);

            MyDictionary._words.Remove(MyDictionary._selectedWord);
            MyDictionary._selectedWord._definition = NewDefinitionTextBox.Text;
            MyDictionary._selectedWord._imagePath = NewImageTextBox.Text;
            if (UpdateExistingCategoryComboBox.Visibility == Visibility.Visible)
            {
                MyDictionary._selectedWord._category = UpdateExistingCategoryComboBox.Text;
            }
            else
            {
                if (MyDictionary._categories != null && MyDictionary._categories.Contains(NewCategoryTextBox.Text))
                {
                    MessageBox.Show("This category already exists!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    MyDictionary.AddCategory(NewCategoryTextBox.Text);
                }
            }
            MyDictionary.AddWord(MyDictionary._selectedWord);

            WordComboBox.Text = "";
            NewDefinitionTextBox.Text = "";
            NewImageTextBox.Text = "";
            NewCategoryTextBox.Text = "";
            UpdateExistingCategoryComboBox.Text = "";
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

            AddCategory_ComboBox(UpdateExistingCategoryComboBox);
        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Fișiere de imagine|*.jpg;*.jpeg;*.png;*.gif|Toate fișierele|*.*";
            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    string newImagePath = openFileDialog.FileName;
                    string destinationPath = System.IO.Path.Combine("..\\..\\Images", System.IO.Path.GetFileName(newImagePath));
                    System.IO.File.Copy(newImagePath, destinationPath, true);

                    // Update the image path
                    ImageToAddTextBox.Text = destinationPath;
                    NewImageTextBox.Text = destinationPath;
                    MyDictionary._selectedWord._imagePath = destinationPath;
                }
                catch (Exception ex)
                {
                    // Display an error message if the file can't be accessed
                    MessageBox.Show($"File can't be accessed!: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void AddCategory_ComboBox(ComboBox comboBoxName)
        {
            comboBoxName.Items.Clear();
            if (MyDictionary._categories == null)
            {
                comboBoxName.Items.Add("No categories");
                return;
            }
            foreach (string category in MyDictionary._categories)
            {
                comboBoxName.Items.Add(category);
            }
            if (MyDictionary._categories.Count > 0)
            {
                comboBoxName.SelectedIndex = 0;
            }
        }
        private void AddWords_ComboBox(ComboBox comboBoxName)
        {
            comboBoxName.Items.Clear();
            foreach (Word word in MyDictionary._words)
            {
                comboBoxName.Items.Add(word._name);
            }
        }
        private void WordComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WordComboBox.SelectedItem != null)
            {
                string selectedWord = WordComboBox.SelectedItem.ToString();
                MyDictionary._selectedWord = null;
                MyDictionary._selectedWord = MyDictionary._words.Find(w => w._name == selectedWord);

                if (MyDictionary._selectedWord != null)
                {
                    NewDefinitionTextBox.Text = MyDictionary._selectedWord._definition;
                    NewImageTextBox.Text = MyDictionary._selectedWord._imagePath;
                    NewCategoryTextBox.Text = MyDictionary._selectedWord._category;
                    UpdateExistingCategoryComboBox.SelectedItem = MyDictionary._selectedWord._category;
                }
                else
                {
                    MessageBox.Show("The selected word does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}