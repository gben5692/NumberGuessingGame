using System;
using System.IO;
using System.Threading;

namespace NumberGuessingGame
{
    internal class NumberGuessingGame
    {
        static void PlayGame(int min, int max, string difficultyName, string userName)
        {
            // Variables Here
            int guesses = 0;
            int userGuess = 0;
            int highScore = int.MaxValue;
            string highScoreUser = "N/A";
            int secretNumber = new Random().Next(min, max + 1);

            // text file to store high score and user name for each difficulty level
            string highScoreFile = $"highscore_{difficultyName}.txt";

            // Messages to display to user based on their guess
            string[] highMessages =
            {
                "Too high! Try again.",
                "Your guess is too high. Give it another shot!",
                "Aim lower! That guess was too high.",
                "That's above the number. Try a smaller one!",
                "You're overshooting it! Guess lower."
                };
            string[] lowMessages =
            {
                "Too low! Try again.",
                "Your guess is too low. Give it another shot!",
                "Aim higher! That guess was too low.",
                "That's below the number. Try a larger one!",
                "You're undershooting it! Guess higher."
                };

            // Load high score from file if it exists
            if (File.Exists(highScoreFile))
            {
                // Read the high score and user name from the file
                string savedData = File.ReadAllText(highScoreFile);
                string[] parts = savedData.Split('|');

                // Parse the high score and user name
                if (parts.Length == 2 && int.TryParse(parts[1], out int loadScore))
                {
                    highScoreUser = parts[0];
                    highScore = loadScore;
                    Console.WriteLine($"Current {difficultyName} high score: {highScore} attempts by {highScoreUser}. Can you beat it?");
                }
            }


            // Game Loop
            while (userGuess != secretNumber)
            {
                // Prompt user for their guess
                Console.WriteLine("Enter your guess: ");
                string input = Console.ReadLine();

                // Validate input and provide feedback
                if (int.TryParse(input, out userGuess))
                {
                    // Check the guess against the secret number
                    if (userGuess > secretNumber)
                    {
                        Console.WriteLine(highMessages[new Random().Next(0, highMessages.Length)]);
                        guesses++;
                    }
                    else if (userGuess < secretNumber)
                    {
                        Console.WriteLine(lowMessages[new Random().Next(0, highMessages.Length)]);
                        guesses++;
                    }
                    else if (userGuess > max || userGuess < min)
                    {
                        Console.WriteLine($"Your guess is out of range. Please guess a number between {min} and {max}.");
                    }
                    else
                    {
                        Console.WriteLine($"Congratulations! You've guessed the number {secretNumber} in {guesses + 1} attempts!");

                        if (guesses + 1 < highScore)
                        {
                            highScore = guesses + 1;
                            Console.WriteLine($"{difficultyName} high score: {highScore} attempts by {userName}!");
                            File.WriteAllText(highScoreFile, $"{userName}|{highScore}");
                        }
                        else
                        {
                            Console.WriteLine($"Current {difficultyName} high score: {highScore} attempts by {highScoreUser}.");
                        }

                    }
                }
                else
                {
                    // Handle invalid input
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

               
            }

            // Ask if the user wants to play again
            Console.WriteLine("\nPlay again? (yes/no)");
            string playAgain = Console.ReadLine();
            if (playAgain.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                Console.Clear();
                Main(new string[] { });
            }
        }

        // Main Method
        static void Main(string[] args)
        {
            // Variables Here

            string author = "gben5692";
            string version = "1.0.0";

            string easyDiffultyString = "1-10";
            string medDiffultyString = "1-100";
            string hardDiffultyString = "1-1000";

            // Welcome Message
            Console.WriteLine("Press Ctrl C to exit ");
            Console.WriteLine("Welcome to the Number Guessing Game!");
            Console.WriteLine("Created by " + author + " | Version: " + version);
            Console.WriteLine("Tell Me you UserName: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Hello " + userName + "! Let's get started.");
            Console.WriteLine($"Choose a difficulty level: Easy: {easyDiffultyString} Medium: {medDiffultyString} Hard: {hardDiffultyString}  ");

            // Get difficulty level from user
            string difficulty = Console.ReadLine();
            if (difficulty.Equals("easy", StringComparison.OrdinalIgnoreCase))
                PlayGame(1, 10, "Easy", userName);
            else if (difficulty.Equals("medium", StringComparison.OrdinalIgnoreCase))
                PlayGame(1, 100, "Medium", userName);
            else if (difficulty.Equals("hard", StringComparison.OrdinalIgnoreCase))
                PlayGame(1, 1000, "Hard", userName);
            else
            {
                // Handle invalid difficulty input
                Console.WriteLine("Invalid difficulty level. Please choose Easy, Medium, or Hard.");
                Thread.Sleep(2000);
                Console.Clear();
                Main(args);
            }
        }

    }
}
