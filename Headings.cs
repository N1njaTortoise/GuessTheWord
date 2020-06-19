using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheWord
{
    class Headings
    {
        private const string gameTitle = "Guess the Word";
        private const string gameTitleLine = "**********************************************";

        public void Welcome()
        {
            Console.WriteLine("\nWelcome to Guess the Word Game." +
                "\n\nTo play the game the blanked out word should be guessed in full or " +
                "\nindividual letters guessed to discover the word - just like hangman!" +
                "\nYou will have a number of tries equal to a third of the word length." +
                "\nIf you get guesses wrong, your tries will decrease up to zero. After which it's game over." +
                "\n\nGood Luck!\n");

            Console.Write("Press enter to continue");
            Console.ReadKey(true);
            Console.Clear();
        }

        public void TitleMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(gameTitle);
            Console.ResetColor();

            Console.WriteLine(gameTitleLine);
        }
    }
}
