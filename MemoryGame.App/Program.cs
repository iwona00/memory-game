using System;
using System.Collections.Generic;

namespace MemoryGame.App
{
    class Program
    {
        private static WordsManager _wordsManager = new WordsManager();
        private static DisplayManager _displayManager = new DisplayManager();
        private static DifficultyLevelManager _difficultyLevelManager = new DifficultyLevelManager();
        private static GameProcessor _gameProcessor = new GameProcessor();

        const string _mismatchDeterminator = "X";

        static void Main(string[] args)
        {
            string[] words = System.IO.File.ReadAllLines(@"C:\Users\Iwcia\Downloads\Words.txt");
            bool playAgain = true;

            do
            {
                Console.WriteLine("Welcome to the Memory Game!");

                var difficulty = _difficultyLevelManager.LoadDifficulty();
                Console.Write($"You chose {difficulty}, so you have to guess location of four word pairs and you have 10 chances to do that.\n");
                var randomArray = _wordsManager.Roll(words, difficulty);

                var doubleWordsArray = _wordsManager.MapToArray(randomArray);
                _wordsManager.Shuffle(doubleWordsArray);

                // Matrix consisting X
                string[,] wordPairs = _wordsManager.Initiate(randomArray);
                string[,] matchedArray = _wordsManager.Initiate(randomArray);
                int chances = _difficultyLevelManager.GetNumberOfChances(difficulty);
                Console.WriteLine($"Level: {difficulty}");
                Console.WriteLine($"Guess chances: {chances}");
                _displayManager.Display(wordPairs);

                bool isAllGuessed = false;
                while (chances > 0 && !isAllGuessed)
                {
                    Console.WriteLine("Write coordinates of the word you want to discover (for example: B2)");

                    var indexColumnPairFirst = _gameProcessor.ReadGuess(wordPairs);
                    wordPairs[indexColumnPairFirst.Key, (indexColumnPairFirst.Value - 1)] = doubleWordsArray[indexColumnPairFirst.Key, (indexColumnPairFirst.Value - 1)];
                    _displayManager.Display(wordPairs);

                    var indexColumnPairSecond = new KeyValuePair<int, int>();
                    bool secondChoiceMatchesFirst;
                    do
                    {
                        indexColumnPairSecond = _gameProcessor.ReadGuess(wordPairs);

                        secondChoiceMatchesFirst = (indexColumnPairSecond.Key.Equals(indexColumnPairFirst.Key) && indexColumnPairSecond.Value.Equals(indexColumnPairFirst.Value)) ||
                            matchedArray[indexColumnPairSecond.Key, (indexColumnPairSecond.Value - 1)].Equals("M") ||
                                matchedArray[indexColumnPairFirst.Key, (indexColumnPairFirst.Value - 1)].Equals("M");

                        if (secondChoiceMatchesFirst)
                        {
                            Console.WriteLine("You chose the same coordinates... -,-");
                        }
                    }
                    while (secondChoiceMatchesFirst);

                    wordPairs[indexColumnPairSecond.Key, (indexColumnPairSecond.Value - 1)] = doubleWordsArray[indexColumnPairSecond.Key, (indexColumnPairSecond.Value - 1)];
                    _displayManager.Display(wordPairs);

                    var firstInputMatchesSecondInput = doubleWordsArray[indexColumnPairSecond.Key, (indexColumnPairSecond.Value - 1)].Equals(doubleWordsArray[indexColumnPairFirst.Key, indexColumnPairFirst.Value - 1]);
                    if (firstInputMatchesSecondInput)
                    {
                        isAllGuessed = _wordsManager.CheckIfAllGuessed(wordPairs);
                        if (isAllGuessed)
                        {
                            Console.WriteLine("Congratulations! You won the game <3");
                            playAgain = _displayManager.ShouldPlayAgain();
                        }
                        else
                        {
                            Console.WriteLine("You matched a pair!");
                            matchedArray[indexColumnPairSecond.Key, (indexColumnPairSecond.Value - 1)] = "M";
                            matchedArray[indexColumnPairFirst.Key, (indexColumnPairFirst.Value - 1)] = "M";
                            if (--chances == 0)
                            {
                                Console.WriteLine("GAME OVER - You lost all of your chances to win the game. Maybe next time you will succeed. Good luck! ^.^");
                                playAgain = _displayManager.ShouldPlayAgain();
                            }
                            else
                            {
                                Console.WriteLine("You still have " + chances + " chances to win.");
                                _displayManager.Display(wordPairs);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("You failed to match. Try again!");
                        if (--chances == 0)
                        {
                            Console.WriteLine("GAME OVER - You lost all of your chances to win the game. Maybe next time you will succeed. Good luck! ^.^");
                            playAgain = _displayManager.ShouldPlayAgain();
                        }
                        else
                        {
                            Console.WriteLine("You still have " + chances + " chances to win.");
                            wordPairs[indexColumnPairSecond.Key, indexColumnPairSecond.Value - 1] = _mismatchDeterminator;
                            wordPairs[indexColumnPairFirst.Key, indexColumnPairFirst.Value - 1] = _mismatchDeterminator;
                            _displayManager.Display(wordPairs);
                        }
                    }
                }
            } while (playAgain);
        }
    }
}

