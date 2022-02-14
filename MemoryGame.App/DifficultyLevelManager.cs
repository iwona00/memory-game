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
    }
}
