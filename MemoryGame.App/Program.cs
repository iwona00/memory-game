using System;
using System.Linq;

namespace MemoryGame.App
{
    class Program
    {

        // Reading each line of the file into a string array. Each element of the array is one line of the file
        string[] words = System.IO.File.ReadAllLines(@"C:\Users\Iwcia\Downloads\Words.txt");

        private static Random rand = new Random();
        string GetRandomWord(string[] words)
        {

            return words[rand.Next(0, words.Length)];



        }

        string[] createRandomWords(int number, string[] words)
        {
            string[] randomWords = new string[number];
            for (int i = 0; i < number; i++)
            {

                string word = GetRandomWord(words);
                while (randomWords.Contains(word))
                {
                    word = GetRandomWord(words);
                }
                randomWords[i] = word;
            }
            return randomWords;
        }


        public static void shuffle(Random random, string[,] doubleWordsArray)
        {
            int lengthRow = doubleWordsArray.GetLength(1);

            for (int i = doubleWordsArray.Length - 1; i > 0; i--)
            {
                int i0 = i / lengthRow;
                int i1 = i % lengthRow;

                int j = random.Next(i + 1);
                int j0 = j / lengthRow;
                int j1 = j % lengthRow;

                string temp = doubleWordsArray[i0, i1];
                doubleWordsArray[i0, i1] = doubleWordsArray[j0, j1];
                doubleWordsArray[j0, j1] = temp;
            }
        }

        public void displayArray(string[,] wordPairs)
        {
            string numbers = "  1 2 3 4";
            Console.WriteLine(numbers);

            Console.Write("A ");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(wordPairs[i, j] + " ");
                }
                Console.Write("\n");
                if (i == 0)
                    Console.Write("B ");
            }
        }

        public void displayLongerArray(string[,] wordPairs)
        {
            string numbers = "  1 2 3 4 5 6 7 8";
            Console.WriteLine(numbers);

            Console.Write("A ");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(wordPairs[i, j] + " ");
                }
                Console.Write("\n");
                if (i == 0)
                    Console.Write("B ");
            }
        }

        public void changeCoordinatedToPlaceInArray(string coordinates, string[,] wordPairs, string[,] doubleArray)
        {
            if (coordinates.StartsWith("A"))
            {
                wordPairs[0, coordinates[1]] = doubleArray[0, coordinates[1]];
            }

            else if (coordinates.StartsWith("B"))
            {
                wordPairs[1, coordinates[1]] = doubleArray[1, coordinates[1]];
            }
        }

        public string[,] displayChoice(string choice, string[,] wordPairs, string[,] doubleArray)
        {

            char firstChar = choice[0];
            char secondChar = choice[1];
            if (firstChar.Equals('A'))
            {
                wordPairs[0, secondChar] = doubleArray[0, secondChar];
            }

            else if (firstChar.Equals('B'))
            {
                wordPairs[1, secondChar] = doubleArray[1, secondChar];
            }

            return wordPairs;

        }


        public bool checkIfAllGuessed(string[,] wordPairs)
        {
            // Checking if all words are uncovered
            bool guessed = false;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (wordPairs[i, j].Equals("X"))
                    {
                        return false;
                    }
                    else
                    {
                        guessed = true;
                    }
                }

            }
            return true;
        }

        public bool checkIfAllGuessedHard(string[,] wordPairs)
        {
            // Checking if all words are uncovered
            bool guessed = false;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (wordPairs[i, j].Equals("X"))
                    {
                        return false;
                    }
                    else
                    {
                        guessed = true;
                    }
                }

            }
            return true;
        }

        public bool doYouWantToPlay()
        {
            Console.Write("Do you want to play again? Write 'Y' if YES or 'N' if NO: ");
            string want = Console.ReadLine();
            if (want.Equals("Y"))
            {
                return true;
            }
            else if (want.Equals("N"))
            {
                return false;
            }
            else
            {
                Console.WriteLine("You wrote a bad letter.");
                return false;
            }
        }


        static void Main(string[] args)
        {
            string[] words = System.IO.File.ReadAllLines(@"C:\Users\Iwcia\Downloads\Words.txt");

            // Displaying the file contents by using a foreach loop.
            System.Console.WriteLine("All words: ");
            foreach (string word in words)
            {
                Console.WriteLine("\t" + word);
            }


            // THE GAME
            bool playAgain = true;
            bool isPaired = false;
            do
            {
                Console.WriteLine("Welcome to the Memory Game!");
                Console.Write("Choose on which difficulty level you want to play - easy (enter 'e') or hard (enter 'h'): ");
                string level = Console.ReadLine();
                if (level.Contains("e"))
                {
                    Console.Write("You chose EASY, so you have to guess location of four word pairs and you have 10 chances to do that.\n");
                    // Generating random words for Easy level
                    Program p = new Program();

                    string[] randomArray = p.createRandomWords(4, words);
                    System.Console.WriteLine("Four random words: ");
                    foreach (string word in randomArray)
                    {
                        Console.WriteLine("\t" + word);
                    }

                    string[,] doubleWordsArray = new string[2, 4] { { randomArray[0], randomArray[1], randomArray[2], randomArray[3] } ,
                    { randomArray[0], randomArray[1], randomArray[2], randomArray[3] } };

                    // Shuffling the array
                    Random rnd = new Random();
                    shuffle(rnd, doubleWordsArray);



                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            Console.Write(doubleWordsArray[i, j] + " ");
                        }
                        Console.Write("\n");

                    }

                    // Matrix consisting X

                    string[,] wordPairs = new string[2, 4] { { "X", "X", "X", "X" }, { "X", "X", "X", "X" } };
                    string[,] matchedArray = new string[2, 4] { { "X", "X", "X", "X" }, { "X", "X", "X", "X" } };
                    Console.WriteLine("Level: easy");
                    Console.WriteLine("Guess chances: 10");
                    p.displayArray(wordPairs);
                    int chances = 10;
                    bool isAllGuessed = false;
                    string choice;
                    string firstLetter;
                    int secondLetter;
                    bool goodChoice = true;
                    bool nextGoodChoice = true;
                    string nextChoice;
                    string firstLetterOfNextChoice;
                    int secondLetterOfNextChoice;
                    while (chances > 0 & isAllGuessed == false)
                    {


                        do
                        {
                            Console.WriteLine("Write coordinates of the word you want to discover (for example: B2)");
                            Console.Write("Your choice: ");


                            choice = Console.ReadLine();

                            firstLetter = choice.Substring(0, 1);
                            secondLetter = int.Parse(choice.Substring(1, 1));


                            int choiceIndex;
                            if (firstLetter.Equals("A"))
                            {
                                choiceIndex = 0;
                            }
                            else
                            {
                                choiceIndex = 1;
                            }
                            if (!(wordPairs[choiceIndex, secondLetter - 1].Equals("X")))
                            {
                                Console.WriteLine("You chose coordinates of a word which is already matched...");
                                goodChoice = false;
                            }
                            else
                            {
                                goodChoice = true;
                            }
                        }
                        while (goodChoice == false);

                        if (firstLetter.Equals("A"))
                        {

                            wordPairs[0, (secondLetter - 1)] = doubleWordsArray[0, (secondLetter - 1)];
                            p.displayArray(wordPairs);
                        }


                        else if (firstLetter.Equals("B"))
                        {
                            wordPairs[1, (secondLetter - 1)] = doubleWordsArray[1, (secondLetter - 1)];
                            p.displayArray(wordPairs);
                        }

                        do
                        {
                            Console.Write("Your next choice: ");
                            nextChoice = Console.ReadLine();
                            firstLetterOfNextChoice = nextChoice.Substring(0, 1);
                            secondLetterOfNextChoice = int.Parse(nextChoice.Substring(1, 1));
                            int nextChoiceIndex1;
                            int choiceIndex1;

                            if (firstLetterOfNextChoice.Equals("A"))
                            {
                                nextChoiceIndex1 = 0;
                            }
                            else
                            {
                                nextChoiceIndex1 = 1;
                            }

                            if (firstLetterOfNextChoice.Equals("A"))
                            {
                                choiceIndex1 = 0;
                            }
                            else
                            {
                                choiceIndex1 = 1;
                            }

                            if (nextChoice.Equals(choice) | matchedArray[nextChoiceIndex1, (secondLetterOfNextChoice - 1)].Equals("M") |
                                    matchedArray[choiceIndex1, (secondLetter - 1)].Equals("M"))
                            {
                                Console.WriteLine("You chose the same coordinates... -,-");
                                nextGoodChoice = false;
                            }
                            else
                            {
                                nextGoodChoice = true;
                            }
                        }
                        while (nextGoodChoice == false);


                        int nextChoiceIndex;
                        int choiceIndexAgain;

                        if (firstLetter.Equals("A"))
                        {
                            choiceIndexAgain = 0;
                        }
                        else
                        {
                            choiceIndexAgain = 1;
                        }


                        if (firstLetterOfNextChoice.Equals("A"))
                        {
                            nextChoiceIndex = 0;
                        }
                        else
                        {
                            nextChoiceIndex = 1;
                        }


                        if (firstLetterOfNextChoice.Equals("A"))
                        {

                            wordPairs[0, (secondLetterOfNextChoice - 1)] = doubleWordsArray[0, (secondLetterOfNextChoice - 1)];
                            p.displayArray(wordPairs);



                            if (doubleWordsArray[nextChoiceIndex, (secondLetterOfNextChoice - 1)].Equals(doubleWordsArray[choiceIndexAgain, secondLetter - 1]))

                            {
                                isAllGuessed = p.checkIfAllGuessed(wordPairs);
                                if (isAllGuessed == true)
                                {
                                    Console.WriteLine("Congratulations! You won the game <3");
                                    playAgain = p.doYouWantToPlay();
                                }
                                else
                                {
                                    Console.WriteLine("You matched a pair!");
                                    matchedArray[nextChoiceIndex, (secondLetterOfNextChoice - 1)] = "M";
                                    matchedArray[choiceIndexAgain, (secondLetter - 1)] = "M";
                                    chances--;
                                    if (chances == 0)
                                    {
                                        Console.WriteLine("GAME OVER - You lost all of your chances to win the game. Maybe next time you will succeed. Good luck! ^.^");
                                        playAgain = p.doYouWantToPlay();
                                    }
                                    else
                                    {
                                        Console.WriteLine("You still have " + chances + " chances to win.");
                                        p.displayArray(wordPairs);
                                    }
                                }

                            }
                            else
                            {
                                Console.WriteLine("You failed to match. Try again!");
                                chances--;
                                if (chances == 0)
                                {
                                    Console.WriteLine("GAME OVER - You lost all of your chances to win the game. Maybe next time you will succeed. Good luck! ^.^");
                                    playAgain = p.doYouWantToPlay();
                                }
                                else
                                {
                                    Console.WriteLine("You still have " + chances + " chances to win.");
                                    wordPairs[choiceIndexAgain, secondLetter - 1] = "X";
                                    wordPairs[nextChoiceIndex, secondLetterOfNextChoice - 1] = "X";
                                    p.displayArray(wordPairs);
                                }
                            }


                        }
                        else if (firstLetterOfNextChoice.Equals("B"))
                        {
                            wordPairs[1, secondLetterOfNextChoice - 1] = doubleWordsArray[1, secondLetterOfNextChoice - 1];
                            p.displayArray(wordPairs);

                            if (doubleWordsArray[nextChoiceIndex, secondLetterOfNextChoice - 1].Equals(doubleWordsArray[choiceIndexAgain, secondLetter - 1]))

                            {
                                isAllGuessed = p.checkIfAllGuessed(wordPairs);
                                if (isAllGuessed == true)
                                {
                                    Console.WriteLine("Congratulations! You won the game <3");
                                    playAgain = p.doYouWantToPlay();

                                }
                                else
                                {
                                    Console.WriteLine("You matched a pair!");
                                    matchedArray[nextChoiceIndex, (secondLetterOfNextChoice - 1)] = "M";
                                    matchedArray[choiceIndexAgain, (secondLetter - 1)] = "M";
                                    chances--;
                                    if (chances == 0)
                                    {
                                        Console.WriteLine("GAME OVER - You lost all of your chances to win the game. Maybe next time you will succeed. Good luck! ^.^");
                                        playAgain = p.doYouWantToPlay();
                                    }
                                    else
                                    {
                                        Console.WriteLine("You still have " + chances + " chances to win.");
                                        p.displayArray(wordPairs);
                                    }
                                }

                            }
                            else
                            {
                                Console.WriteLine("You failed to match. Try again!");
                                chances--;
                                if (chances == 0)
                                {
                                    Console.WriteLine("GAME OVER - You lost all of your chances to win the game. Maybe next time you will succeed. Good luck! ^.^");
                                    playAgain = p.doYouWantToPlay();
                                }
                                else
                                {
                                    Console.WriteLine("You still have " + chances + " chances to win.");
                                    wordPairs[choiceIndexAgain, secondLetter - 1] = "X";
                                    wordPairs[nextChoiceIndex, secondLetterOfNextChoice - 1] = "X";
                                    p.displayArray(wordPairs);
                                }
                            }
                        }



                    }
                }

                else if (level.Contains("h"))
                {
                    Console.Write("You chose HARD, so you have to guess location of eight word pairs and you have 15 chances to do that.\n");
                    // Generating random words for Hard level
                    Program pr = new Program();

                    string[] newRandomArray = pr.createRandomWords(8, words);
                    System.Console.WriteLine("Eight random words: ");
                    foreach (string word in newRandomArray)
                    {
                        Console.WriteLine("\t" + word);
                    }

                    string[,] doubleWordsLongerArray = new string[2, 8]
                    {
                    {newRandomArray[0],newRandomArray[1], newRandomArray[2], newRandomArray[3],
                    newRandomArray[4], newRandomArray[5], newRandomArray[6], newRandomArray[7]}, {newRandomArray[0],newRandomArray[1], newRandomArray[2], newRandomArray[3],
                    newRandomArray[4], newRandomArray[5], newRandomArray[6], newRandomArray[7],
                     }
                    };

                    // Shuffling the array
                    Random rndo = new Random();
                    shuffle(rndo, doubleWordsLongerArray);



                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            Console.Write(doubleWordsLongerArray[i, j] + " ");
                        }
                        Console.Write("\n");

                    }

                    // Matrix consisting X

                    string[,] wordPairsHard = new string[2, 8] { { "X", "X", "X", "X", "X", "X", "X", "X" }, { "X", "X", "X", "X", "X", "X", "X", "X" } };
                    string[,] matchedArrayHard = new string[2, 8] { { "X", "X", "X", "X", "X", "X", "X", "X" }, { "X", "X", "X", "X", "X", "X", "X", "X" } };

                    Console.WriteLine("Level: hard");
                    Console.WriteLine("Guess chances: 15");
                    pr.displayLongerArray(wordPairsHard);
                    int chancesHard = 15;
                    bool isAllGuessedHard = false;
                    string choiceHard;
                    string firstLetterHard;
                    int secondLetterHard;
                    bool goodChoiceHard = true;
                    bool nextGoodChoiceHard = true;
                    string nextChoiceHard;
                    string firstLetterOfNextChoiceHard;
                    int Hard;
                    while (chancesHard > 0 & isAllGuessedHard == false)
                    {


                        do
                        {
                            Console.WriteLine("Write coordinates of the word you want to discover (for example: B2)");
                            Console.Write("Your choice: ");


                            choiceHard = Console.ReadLine();

                            firstLetterHard = choiceHard.Substring(0, 1);
                            secondLetterHard = int.Parse(choiceHard.Substring(1, 1));


                            int choiceIndex;
                            if (firstLetterHard.Equals("A"))
                            {
                                choiceIndex = 0;
                            }
                            else
                            {
                                choiceIndex = 1;
                            }
                            if (!(wordPairsHard[choiceIndex, secondLetterHard - 1].Equals("X")))
                            {
                                Console.WriteLine("You chose coordinates of a word which is already matched...");
                                goodChoiceHard = false;
                            }
                            else
                            {
                                goodChoiceHard = true;
                            }
                        }
                        while (goodChoiceHard == false);

                        if (firstLetterHard.Equals("A"))
                        {
                            int choiceIndex = 0;
                            wordPairsHard[choiceIndex, (secondLetterHard - 1)] = doubleWordsLongerArray[choiceIndex, (secondLetterHard - 1)];
                            pr.displayLongerArray(wordPairsHard);
                        }


                        else if (firstLetterHard.Equals("B"))
                        {
                            int choiceIndexB = 1;
                            wordPairsHard[choiceIndexB, (secondLetterHard - 1)] = doubleWordsLongerArray[choiceIndexB, (secondLetterHard - 1)];
                            pr.displayLongerArray(wordPairsHard);
                        }

                        do
                        {
                            Console.Write("Your next choice: ");
                            nextChoiceHard = Console.ReadLine();
                            firstLetterOfNextChoiceHard = nextChoiceHard.Substring(0, 1);
                            Hard = int.Parse(nextChoiceHard.Substring(1, 1));
                            int nextChoiceIndex1;
                            int choiceIndex1;

                            if (firstLetterHard.Equals("A"))
                            {
                                choiceIndex1 = 0;
                            }
                            else
                            {
                                choiceIndex1 = 1;
                            }

                            if (firstLetterOfNextChoiceHard.Equals("A"))
                            {
                                nextChoiceIndex1 = 0;
                            }
                            else
                            {
                                nextChoiceIndex1 = 1;
                            }

                            if (nextChoiceHard.Equals(choiceHard) | matchedArrayHard[nextChoiceIndex1, (Hard - 1)].Equals("M") |
                                    matchedArrayHard[choiceIndex1, (secondLetterHard - 1)].Equals("M"))
                            {
                                Console.WriteLine("You chose the same coordinates... -,-");
                                nextGoodChoiceHard = false;
                            }
                            else
                            {
                                nextGoodChoiceHard = true;
                            }
                        }
                        while (nextGoodChoiceHard == false);


                        int nextChoiceIndex;
                        int choiceIndexAgain;

                        if (firstLetterHard.Equals("A"))
                        {
                            choiceIndexAgain = 0;
                        }
                        else
                        {
                            choiceIndexAgain = 1;
                        }


                        if (firstLetterOfNextChoiceHard.Equals("A"))
                        {
                            nextChoiceIndex = 0;
                        }
                        else
                        {
                            nextChoiceIndex = 1;
                        }


                        if (firstLetterOfNextChoiceHard.Equals("A"))
                        {

                            wordPairsHard[0, (Hard - 1)] = doubleWordsLongerArray[0, (Hard - 1)];
                            pr.displayLongerArray(wordPairsHard);



                            if (doubleWordsLongerArray[nextChoiceIndex, (Hard - 1)].Equals(doubleWordsLongerArray[choiceIndexAgain, secondLetterHard - 1]))

                            {
                                isAllGuessedHard = pr.checkIfAllGuessed(wordPairsHard);
                                if (isAllGuessedHard == true)
                                {
                                    Console.WriteLine("Congratulations! You won the game <3");
                                    playAgain = pr.doYouWantToPlay();
                                }
                                else
                                {
                                    Console.WriteLine("You matched a pair!");
                                    matchedArrayHard[nextChoiceIndex, (Hard - 1)] = "M";
                                    matchedArrayHard[choiceIndexAgain, (secondLetterHard - 1)] = "M";
                                    chancesHard--;
                                    if (chancesHard == 0)
                                    {
                                        Console.WriteLine("GAME OVER - You lost all of your chances to win the game. Maybe next time you will succeed. Good luck! ^.^");
                                        playAgain = pr.doYouWantToPlay();
                                    }
                                    else
                                    {
                                        Console.WriteLine("You still have " + chancesHard + " chances to win.");
                                        pr.displayLongerArray(wordPairsHard);
                                    }
                                }

                            }
                            else
                            {
                                Console.WriteLine("You failed to match. Try again!");
                                chancesHard--;
                                if (chancesHard == 0)
                                {
                                    Console.WriteLine("GAME OVER - You lost all of your chances to win the game. Maybe next time you will succeed. Good luck! ^.^");
                                    playAgain = pr.doYouWantToPlay();
                                }
                                else
                                {
                                    Console.WriteLine("You still have " + chancesHard + " chances to win.");
                                    wordPairsHard[choiceIndexAgain, secondLetterHard - 1] = "X";
                                    wordPairsHard[nextChoiceIndex, Hard - 1] = "X";
                                    pr.displayLongerArray(wordPairsHard);
                                }
                            }


                        }
                        else if (firstLetterOfNextChoiceHard.Equals("B"))
                        {
                            wordPairsHard[1, Hard - 1] = doubleWordsLongerArray[1, Hard - 1];
                            pr.displayLongerArray(wordPairsHard);

                            if (doubleWordsLongerArray[nextChoiceIndex, Hard - 1].Equals(doubleWordsLongerArray[choiceIndexAgain, secondLetterHard - 1]))

                            {
                                isAllGuessedHard = pr.checkIfAllGuessedHard(wordPairsHard);
                                if (isAllGuessedHard == true)
                                {
                                    Console.WriteLine("Congratulations! You won the game <3");
                                    playAgain = pr.doYouWantToPlay();

                                }
                                else
                                {
                                    Console.WriteLine("You matched a pair!");
                                    matchedArrayHard[nextChoiceIndex, (Hard - 1)] = "M";
                                    matchedArrayHard[choiceIndexAgain, (secondLetterHard - 1)] = "M";
                                    chancesHard--;
                                    if (chancesHard == 0)
                                    {
                                        Console.WriteLine("GAME OVER - You lost all of your chances to win the game. Maybe next time you will succeed. Good luck! ^.^");
                                        playAgain = pr.doYouWantToPlay();
                                    }
                                    else
                                    {
                                        Console.WriteLine("You still have " + chancesHard + " chances to win.");
                                        pr.displayLongerArray(wordPairsHard);
                                    }
                                }

                            }
                            else
                            {
                                Console.WriteLine("You failed to match. Try again!");
                                chancesHard--;
                                if (chancesHard == 0)
                                {
                                    Console.WriteLine("GAME OVER - You lost all of your chances to win the game. Maybe next time you will succeed. Good luck! ^.^");
                                    playAgain = pr.doYouWantToPlay();
                                }
                                else
                                {
                                    Console.WriteLine("You still have " + chancesHard + " chances to win.");
                                    wordPairsHard[choiceIndexAgain, secondLetterHard - 1] = "X";
                                    wordPairsHard[nextChoiceIndex, Hard - 1] = "X";
                                    pr.displayLongerArray(wordPairsHard);
                                }
                            }
                        }



                    }
                }


                else
                {
                    Console.WriteLine("You entered bad letter, try again...");

                }


            } while (playAgain == true);


        }













    }


}

