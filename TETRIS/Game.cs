using System;
using System.Collections.Generic;
using System.Text;

namespace TETRIS
{
    public class Game       //klasa zawierająca doadtkowe funkcjonalności związane z rozgrywką
    {
        private int score;
        private bool gameState;
        private int delay;
        private int difficulty;


        // gettery i settery pól
        public int Score            
        {
            get
            {
                return score;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                score += value*10;
            }
        }

        public bool GameState
        {
            get
            {
                return gameState;
            }
            set
            {
                gameState = value;
            }
        }

        public int Delay
        {
            get
            {
                return delay;
            }
            set
            {
                if (value < 50 || value >5000)
                {
                    value = 50;
                }
                delay = value;
            }
        }
        public int Difficulty
        {
            get
            {
                return difficulty;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                difficulty = value;
            }
        }

        public Game(int d)
        {
            Score = 0;
            Difficulty = 1;
            GameState = true;
            Delay = d;
        }

        //pomocnicze metody
        public void ShowStats(Board Board)
        {
            Console.SetCursorPosition((Board.Width*2) + 2, 0);
            Console.Write("SCORE: " + score);
            Console.SetCursorPosition((Board.Width*2) + 2, 1);
            Console.Write("LEVEL: " + difficulty);
        }

        public void Start(Board Board)
        {
            Console.SetCursorPosition((Board.Width * 2) + 2, 5);
            Console.Write("Press Any Key To Start");
            Console.ReadKey();
            Console.SetCursorPosition((Board.Width * 2) + 2, 5);
            Console.Write("                      ");
        }
        public void ClearInputBuffer()
        {
            if (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
        }

        public void ModifySpeed(int startDelay)
        {
            Delay = startDelay - difficulty * 50;
        }
        public void GameOver(Board Board)
        {
            Console.SetCursorPosition((Board.Width*2) +2, 5);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("******GAME OVER*******");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, Board.Height + 1);
            Console.ReadKey();
        }

    }
}
