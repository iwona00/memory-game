using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGame.App
{
    public class WordsManager
    {
        private static Random rng = new Random();

        private DifficultyLevelManager _difficultyLevelManager;

        public WordsManager()
        {
            _difficultyLevelManager = new DifficultyLevelManager();
        }

        public List<string> Roll(string[] words, Difficulty difficulty)
        {
            var number = _difficultyLevelManager.GetNumberOfChoices(difficulty);
            return words.Take(number).ToList();
        }

        public void Shuffle(string[,] doubleWordsArray)
        {
            int lengthRow = doubleWordsArray.GetLength(1);

            for (int i = doubleWordsArray.Length - 1; i > 0; i--)
            {
                int i0 = i / lengthRow;
                int i1 = i % lengthRow;

                int j = rng.Next(i + 1);
                int j0 = j / lengthRow;
                int j1 = j % lengthRow;

                string temp = doubleWordsArray[i0, i1];
                doubleWordsArray[i0, i1] = doubleWordsArray[j0, j1];
                doubleWordsArray[j0, j1] = temp;
            }
        }

        public string[,] MapToArray(List<string> words)
        {
            const int rows  = 2;
            var columns = words.Count;

            var result = new string[rows, columns];
            for (int r = 0; r != rows; r++)
                for (int c = 0; c != columns; c++)
                    result[r, c] = words[c];

            return result;
        }

        public string[,] Initiate(List<string> words)
        {
            const int rows = 2;
            var columns = words.Count;

            var result = new string[rows, columns];
            for (int r = 0; r != rows; r++)
                for (int c = 0; c != columns; c++)
                    result[r, c] = "X";

            return result;
        }

        public bool CheckIfAllGuessed(string[,] wordPairs)
        {
            var rowsCount = wordPairs.GetLength(0);
            var columnsCount = wordPairs.GetLength(1);

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    if (wordPairs[i, j].Equals("X"))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
