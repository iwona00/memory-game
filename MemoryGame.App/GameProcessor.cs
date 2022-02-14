using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGame.App
{
    public class GameProcessor
    {
        public KeyValuePair<int, int> ReadGuess (string[,] wordPairs)
        {
            var goodChoice = false;
            var alphabetFirstLetterIndex = 65;
            const string mismatchDeterminator = "X";

            do
            {
                Console.Write("Your choice: ");
                var choice = Console.ReadLine();

                var parsedInput = choice.Substring(0, 1).ToCharArray().First();
                if (!int.TryParse(choice.Substring(1, 1), out var parsedColumn))
                {
                    Console.WriteLine("Wrong! Enter correct value.");
                    continue;
                }

                var choiceIndex = parsedInput - alphabetFirstLetterIndex;
                if (choiceIndex < 0 || choiceIndex > 1 || parsedColumn <1 || parsedColumn > wordPairs.GetLength(1))
                {
                    Console.WriteLine("Wrong! Enter correct value.");
                    continue;
                }

                var alreadyMatched = wordPairs[choiceIndex, parsedColumn - 1].Equals(mismatchDeterminator);
                if (!alreadyMatched)
                {
                    Console.WriteLine("You chose coordinates of a word which is already matched...");
                    continue;
                }

                return new KeyValuePair<int, int>(choiceIndex, parsedColumn);
            }
            while (goodChoice == false);

            throw new Exception("Cannot determinate the user inuput.");
        }
    }
}
