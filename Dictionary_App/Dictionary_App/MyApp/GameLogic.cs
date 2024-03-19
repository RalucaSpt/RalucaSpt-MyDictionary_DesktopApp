using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary_App.MyApp
{
    static class GameLogic
    {
        static public List<Word> _wordsToGuess= new List<Word>();
        static public int _score;
        static public int _currentRound;

        static GameLogic()
        {
            SelectWords();
        }

        //select 5 words randomly from the list of words
        public static void SelectWords()
        {
            Random random = new Random();
            List<Word> allWords = MyDictionary._words;
            for (int i = 0; i < 5; i++)
            {
                int index = random.Next(0, allWords.Count);
                _wordsToGuess.Add(allWords[index]);
                allWords.RemoveAt(index);
            }
        }
    }
}
