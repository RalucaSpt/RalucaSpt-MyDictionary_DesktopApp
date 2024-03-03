using Dictionary_App.MyApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary_App.MyApp
{
    internal class MyDictionary
    {
        Dictionary<string, List<Word>> categoryMap;
        // Constructor
        public MyDictionary()
        {
            categoryMap = new Dictionary<string, List<Word>>();
        }
        // Add a word to the category map
        public void AddWord(string category, Word word)
        {
            if (!categoryMap.ContainsKey(category))
            {
                categoryMap.Add(category, new List<Word>());
            }
            categoryMap[category].Add(word);
        }
        //delete a word from the category map
        public void DeleteWord(Word word)
        {
            foreach (var category in categoryMap)
            {
                if (category.Value.Contains(word))
                {
                    category.Value.Remove(word);
                }
            }
        }
        //update a word in the category map
        public void UpdateWord(Word oldWord, Word newWord)
        {
            foreach (var category in categoryMap)
            {
                if (category.Value.Contains(oldWord))
                {
                    category.Value.Remove(oldWord);
                    category.Value.Add(newWord);
                }
            }
        }
        //update a word in the category map
        public void UpdateWord(string category, Word oldWord, Word newWord)
        {
            if (categoryMap.ContainsKey(category))
            {
                if (categoryMap[category].Contains(oldWord))
                {
                    categoryMap[category].Remove(oldWord);
                    categoryMap[category].Add(newWord);
                }
            }
        }

        // Get the list of words for a category
        public List<Word> GetWords(string category)
        {
            if (categoryMap.ContainsKey(category))
            {
                return categoryMap[category];
            }
            return null;
        }

        //Get a word from a category
        public Word GetWord(string category, string word)
        {
            if (categoryMap.ContainsKey(category))
            {
                foreach (var w in categoryMap[category])
                {
                    if (w._name == word)
                    {
                        return w;
                    }
                }
            }
            return null;
        }

        //Get a word 
        public Word GetWord(string word)
        {
            foreach (var category in categoryMap)
            {
                foreach (var w in category.Value)
                {
                    if (w._name == word)
                    {
                        return w;
                    }
                }
            }
            return null;
        }
    }
}