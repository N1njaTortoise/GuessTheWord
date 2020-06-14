using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheWord
{
    class Program
    {
        static void Main(string[] args)
        {
            GameCore game = new GameCore();

            do
            {
                game.Play();

            } while (game.PlayAgain());
        }
    }
}
