using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheWord
{
    class Words
    {
        private Random rng;
        private string _generatedWord;

        public Words()
        {
            rng = new Random();
        }

        /*Class to allow an input of new words and store to .txt file
         or have words searched from a list of the .txt file*/

        List<string> wordDictionary = new List<string>() { "house", "dog", "cat", "hospital", "church", "school", "shark", "computer" };
        
        public string GeneratedWordFromDictionary()
        {
            _generatedWord = wordDictionary[rng.Next(0, wordDictionary.Count)];
            return _generatedWord;
        }
    }
}
