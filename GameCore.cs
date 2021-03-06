﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuessTheWord
{
    class GameCore
    {
        private string _guessedWordFromDictionary;
        private string playAgain;

        private List<string> _alreadyChosenLetters = new List<string>();
        Words dictionary = new Words();
        Headings headings = new Headings();


        private string _wordToGuessBlanked;
        private string _userInput = null;
        private string _wordToDisplay;

        private char[] maskedWord;

        private int _numberOfGuesses;

        public void Play()
        {
            Setup();
            
            _numberOfGuesses = _guessedWordFromDictionary.Length / 2;

            while (WinningCondition())
            {
                Console.WriteLine(_guessedWordFromDictionary);
                headings.TitleMenu();
                //TitleMenu();
                
                DisplayWord();

                _userInput = UserInput();
                HiddenWordReveal();
            }
        }

        private void DisplayAlreadGuessedWords()
        {
            Console.Write("Already guessed words: ");
            Console.WriteLine(string.Join(",", _alreadyChosenLetters));
        }

        public bool PlayAgain()
        {
            while (true)
            {
                Console.Write("\nWould you like to play again Y/N: ");
                playAgain = Console.ReadLine().ToLower();

                if (playAgain == "y")
                {
                    _guessedWordFromDictionary = string.Empty;
                    _alreadyChosenLetters.Clear();
                    _wordToGuessBlanked = string.Empty;
                    _wordToDisplay = string.Empty;

                    Console.Clear();

                    return true;
                }
                else if (playAgain == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid input.");

                    Thread.Sleep(1500);
                    Console.Clear();

                    headings.TitleMenu();
                }
            }
        }

        private bool WinningCondition()
        {
            // NEED TO CHANGE THE MASKED CHAR ARRAY TO A STRING TO ALLOW COMPARING
            if (_wordToDisplay.Equals(_guessedWordFromDictionary) && _numberOfGuesses >= 0)
            {
                WinOrLooseStatement("Win");
                
                return false;
            }
            else if (_numberOfGuesses >= 0)
            {
                Console.Clear();

                return true;
            }
            else if (_numberOfGuesses < 0)
            {
                WinOrLooseStatement("Loose");

                return false;
            }
            else
            {
                Console.Clear();

                return false;
            }
        }

        private void WinOrLooseStatement(string word)
        {
            Console.Clear();

            headings.TitleMenu();

            Console.WriteLine($"The word was : {_guessedWordFromDictionary}");
            Console.WriteLine($"\nYou {word}!");
        }

        // Gets the user input and checks if this value was already selected 
        private string UserInput()
        {
            while (true)
            {
                Console.Clear();

                //SHOWS WORD FOR TESTING
                //Console.WriteLine(_guessedWordFromDictionary);

                headings.TitleMenu();
                DisplayWord();
                Console.WriteLine($"\nNumber of tries remaining: {_numberOfGuesses}");
                DisplayAlreadGuessedWords();

                Console.Write("\nGuess the word or a letter: ");
                _userInput = Console.ReadLine().ToLower();

                if (_userInput.Length == _guessedWordFromDictionary.Length)
                {
                    if (UserUsageChecker(_userInput))
                    {
                        return _userInput;
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (_userInput.Length == 1 && Char.IsLetter(_userInput[0]))
                {
                    if (UserUsageChecker(_userInput))
                    {
                        return _userInput;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid input.");
                    _numberOfGuesses--;

                    if (_numberOfGuesses >= 0)
                    {
                        InvalidInputDisplay();
                    }
                    else
                    {
                        return "outOfTries";
                    }
                }
            }
        }

        private void InvalidInputDisplay()
        {
            Thread.Sleep(1500);
            Console.Clear();

            headings.TitleMenu();
            DisplayWord();
            Console.WriteLine($"Number of tries remaining: {_numberOfGuesses}");
            Console.WriteLine(string.Join(",", _alreadyChosenLetters));
        }

        // Checks to see if input was already selected
        private bool UserUsageChecker(string userInput)
        {
            if (_alreadyChosenLetters.Contains(userInput))
            {
                Console.WriteLine($"\nYou have already guessed this before: {userInput}");

                Console.ReadKey();
                Console.Clear();

                return false;
            }
            else
            {
                _alreadyChosenLetters.Add(userInput);

                return true;
            }
        }

        private void HiddenWordReveal()
        {
            if (_guessedWordFromDictionary.Equals(_userInput))
            {
                for (int i = 0; i < _guessedWordFromDictionary.Length; i++)
                {
                    maskedWord[i] = _userInput[i];
                }
                _wordToDisplay = new string(maskedWord);
            }
            else if (_guessedWordFromDictionary.Contains(_userInput))
            {
                for (int i = 0; i < _guessedWordFromDictionary.Length; i++)
                {
                    if (_guessedWordFromDictionary[i] == _userInput[0])
                    {
                        maskedWord[i] = _userInput[0];
                    }
                }
                _wordToDisplay = new string(maskedWord);
            }
            else
            {
                NumberOfTriesLeft();
            }
        }

        private void NumberOfTriesLeft()
        {
            _numberOfGuesses--;
        }

        private void Setup()
        {
            Console.Title = "Guess the Word Game";

            headings.TitleMenu();
            headings.Welcome();

            // Instantiate word dictionary
            CallGeneratedWord();
        }

        private void CallGeneratedWord()
        {
            dictionary.AddNewWords();

            _guessedWordFromDictionary = dictionary.GeneratedWord;

            _wordToGuessBlanked = new string('-', _guessedWordFromDictionary.Length);

            maskedWord = _wordToGuessBlanked.ToCharArray();

            _wordToDisplay = new string(maskedWord);
        }

        private void DisplayWord()
        {
            Console.Write("The word to guess is : ");
            Console.WriteLine(string.Join("", maskedWord));
        }
    }
}
