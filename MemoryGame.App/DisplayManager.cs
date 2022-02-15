using System;
using System.Linq;

namespace MemoryGame.App
{
    public class DisplayManager
    {
        public void Display(string[,] wordPairs)
        {
            var rowsCount = wordPairs.GetLength(0);
            var columnsCount = wordPairs.GetLength(1);
            int alphabetLetterIndex = 65;

            var displayNumbers = string.Join(" ", Enumerable.Range(1, columnsCount).Select(n => $"{n}"));
            Console.WriteLine(string.Concat("  ", displayNumbers));
            for (int i = 0; i < rowsCount; i++)
            {
                Console.Write($"{(char)alphabetLetterIndex++} ");
                for (int j = 0; j < columnsCount; j++)
                {
                    Console.Write(wordPairs[i, j] + " ");
                }
                Console.Write("\n");
            }
        }

        public bool ShouldPlayAgain()
        {
            Console.Write("Do you want to play again? Write 'Y' if YES or 'N' if NO: ");
            string want = Console.ReadLine();

            switch(want)
            {
                case "Y":
                    return true;
                case "N":
                    return false;
                default:
                    Console.WriteLine("You wrote a bad letter.");
                    return false;
            }
        }
    }
}
