using System;

namespace MemoryGame.App
{
    public class DifficultyLevelManager
    {
        public int GetNumberOfChoices(Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.Easy => 4,
                Difficulty.Hard => 8,
                _ => throw new Exception($"Unrecognized difficulty level: {difficulty}")
            };
        }

         public int GetNumberOfChances(Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.Easy => 10,
                Difficulty.Hard => 15,
                _ => throw new Exception($"Unrecognized difficulty level: {difficulty}")
            };
        }

        public Difficulty LoadDifficulty()
        {
            const string wrongChoiceDisplay = "Wrong! Choose E or H!";

            var difficulty = Difficulty.Unknown;
            do {
                Console.Write("Choose on which difficulty level you want to play - easy (enter 'e') or hard (enter 'h'): ");
                try
                {
                    difficulty = Console.ReadLine() switch
                    {
                        var x when x.ToLower().Equals("e") => Difficulty.Easy,
                        var x when x.ToLower().Equals("h") => Difficulty.Hard,
                        _ => Difficulty.Unknown
                    };
                }
                catch (Exception)
                {
                    Console.WriteLine(wrongChoiceDisplay);
                    continue;
                }
                if (difficulty == Difficulty.Unknown) Console.WriteLine(wrongChoiceDisplay);
            } while (difficulty == default(Difficulty));

            return difficulty;
        }
    }
}
