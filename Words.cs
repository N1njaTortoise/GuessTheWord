using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace GuessTheWord
{
    class Words
    {
        private Random rng;
        private string _generatedWord;
        private bool wordCheckedAndPassed = false;
        private string[] startingWords = { "house", "dog", "cat", "hospital", "church", "school", "shark" };

        Headings headings = new Headings();

        public Words()
        {
            rng = new Random();
        }

        public string GeneratedWord
        {
            get => _generatedWord;    
        }

        string[] wordFromFile;

        public void AddNewWords()
        {
            while (!wordCheckedAndPassed)
            {
                headings.TitleMenu();

                ReadFile();

                string selectedOption = AddWordListMenuInput();

                if (selectedOption.Equals("1"))
                {
                    string newWord = CheckWordHasNumber() + Environment.NewLine;
                    WriteFile(newWord);

                    Console.Clear();
                }
                else
                {
                    GeneratedWordFromDictionary();
                }
            }
            // RESETS THE LOOP FOR WHEN THE PLAYER WANTS TO PLAY AGAIN
            wordCheckedAndPassed = false;
        }

        private void ReadFile()
        {
            if (!File.Exists("WordList.txt"))
            {
                File.AppendAllLines("WordList.txt", startingWords);

                wordFromFile = File.ReadAllLines("WordList.txt");
            }
            else
            {
                wordFromFile = File.ReadAllLines("WordList.txt");
            }
        }

        private void WriteFile(string appendWord)
        {
            File.AppendAllText("WordList.txt", appendWord);
        }

        private string CheckWordHasNumber()
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Enter a word to place into the word list: ");
                string newWordTemp = Console.ReadLine().ToLower().Trim();

                bool wordIsAProperWord = true;

                for (int i = 0; i < newWordTemp.Length; i++)
                {
                    if (!Char.IsLetter(newWordTemp[i]))
                    {
                        wordIsAProperWord = false;
                        ErrorMessage("word");
                        ClearConsoleLines(2);
                        break;
                    }
                }

                if (wordIsAProperWord.Equals(true))
                {
                    return newWordTemp;
                }
            }
        }

        private string AddWordListMenuInput()
        {
            Console.WriteLine("What would you like to do:");
            Console.WriteLine("1 - Add new word to list");
            Console.WriteLine("2 - Play using a randomly selected word from list");

            while (true)
            {
                Console.WriteLine();
                Console.Write("Enter your selection: ");
                string selectedOptionTemp = Console.ReadLine();

                if (selectedOptionTemp.Equals("1") || selectedOptionTemp.Equals("2"))
                {
                    return selectedOptionTemp;
                }
                else
                {
                    ErrorMessage("menu option");
                    ClearConsoleLines(2);
                }
            }
        }

        private void GeneratedWordFromDictionary()
        {
            if (wordFromFile.Length == 0)
            {
                ErrorMessage("word - There are NO current words in the file!");
                Console.Clear();
                wordCheckedAndPassed = false;
            }
            else
            {
                _generatedWord = wordFromFile[rng.Next(0, wordFromFile.Length)];
                wordCheckedAndPassed = true;
            }
        }

        private void ClearConsoleLines(int argLinesRemove)
        {
            int currentBufferCursor = Console.CursorTop - argLinesRemove - 1;

            for (int i = 1; i <= argLinesRemove ; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop - i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, currentBufferCursor);
        }

        private void ErrorMessage(string errorMessage)
        {
            Console.Beep();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Please enter a valid {errorMessage}.");
            Console.ResetColor();
            Thread.Sleep(1500);
        }
    }
}
