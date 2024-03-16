using Dictionary_App.MyApp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;


namespace Dictionary_App.MyApp
{
    public static class MyDictionary
    {

        static MyDictionary()
        {
            Initialize();
        }

        public static void Initialize()
        {
            if (File.Exists("../../Json/category.json"))
            {
                _categories = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("../../Json/category.json"));
            }

            if (File.Exists("../../Json/word.json"))
            {
                _words = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("../../Json/word.json"));
            }
            _selectedWord = new Word();
        }

        public static void SaveWordsToJson()
        {
            File.WriteAllText("../../Json/word.json", JsonConvert.SerializeObject(_words, Formatting.Indented) + Environment.NewLine);
            MessageBox.Show("Data been inserted!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        public static void SaveCategoriesToJson()
        {
            File.WriteAllText("../../Json/category.json", JsonConvert.SerializeObject(_categories, Formatting.Indented) + Environment.NewLine);
        }

        public static void AddWord(Word word)
        {
            _words.Add(word);
            SaveWordsToJson();
        }

        public static void AddCategory(string category)
        {
            _categories.Add(category);
            SaveCategoriesToJson();
        }

        public static void RemoveWord(Word word)
        {
            _words.Remove(word);
            SaveWordsToJson();
        }

        public static void RemoveCategory(string category)
        {
            _categories.Remove(category);
            SaveCategoriesToJson();
        }

        public static void UpdateWord(Word word)
        {
            var index = _words.FindIndex(w => w._name == word._name);
            _words[index] = word;
            SaveWordsToJson();
        }

        public static void UpdateCategory(string oldCategory, string newCategory)
        {
            var index = _categories.FindIndex(c => c == oldCategory);
            _categories[index] = newCategory;
            SaveCategoriesToJson();
        }

        public static void VerifyImageExists()
        {
            if (_selectedWord._imagePath == null)
            {
                _selectedWord._imagePath = "..\\Images\\no_image.jpg";
            }
        }

        public static void DeleteWord(string word)
        {
            _words.RemoveAll(w => w._name == word);
            SaveWordsToJson();
        }

        public static void GetSuggestedWords(string category)
        {
            _suggestedWords = MyDictionary._words.Where(word => word._category == category).ToList();
        }

        public static List<Word> _words { get; set; }
        public static List<string> _categories { get; set; }
        public static List<Word> _suggestedWords { get; set; }
        public static Word _selectedWord { get; set; }
    }
}