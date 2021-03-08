using System;
using System.Collections.Generic;
using System.Text;

namespace TETRIS
{
    public class Board      //klasa opisująca parametry i działanie pola gry
    {
        private int width;
        private int height;
        private int[,] gameBoard;


        //gettery i settery
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value < 10)
                {
                    value = 10;
                }
                width = value;
            }
        }
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value < 22)
                {
                    value = 22;
                }
                height = value;
            }
        }
        public int[,] GameBoard
        {
            get
            {
                return gameBoard;
            }    
        }
        public Board(int w, int h)
        {
            Width = w;
            Height = h;
            gameBoard = new int[Height, Width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if(j>0 && j<width-1 && i<height - 1)
                    {
                        gameBoard[i, j] = 0;
                    }
                    else
                    {
                        gameBoard[i, j] = 1;
                    }
                }
                // inicjalizacja scian i pola gry
            }
        }

        public void Draw()
        {
            //metoda sluząca do wyswietlania pola gry wraz z zapisaną zawartością
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    switch (gameBoard[i,j])
                    {
                        case 0:
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.Write("  ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            }
                        case 1:
                            {
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                Console.Write("  ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            }
                        case 2:
                            {
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.Write("  ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            }
                        case 3:
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.Write("  ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            }
                        case 4:
                            {
                                Console.BackgroundColor = ConsoleColor.Cyan;
                                Console.Write("  ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            }
                        case 5:
                            {
                                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                                Console.Write("  ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            }
                        case 6:
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.Write("  ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            }
                        case 7:
                            {
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.Write("  ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            }
                    }
                }
                Console.Write("\n");
            }
        }
        

        //pomocnicza metoda do nadpisywania tablicy
        public void Override(int y, int x, int value)
        {
            gameBoard[y, x] = value;
        }


        //metoda sprawdzająca czy nie ułożono linii z klocków, usuwająca ową linię, przenosząca elementy wyżej w dół i wywołująca dodanie punktów
        public void RemoveLines(Game Game)
        {
            int elementsInLine;
            for (int i = 0; i < height - 1; i++)
            {
                elementsInLine = 0;
                for (int j = 1; j < width - 1; j++)
                {
                    if (gameBoard[i, j] != 0)
                    {
                        elementsInLine++;
                    }
                    if (elementsInLine == width - 2)
                    {
                        for (int k = 1; k < width - 1; k++)
                        {
                            gameBoard[i, k] = 0;
                        }
                    }
                }
                if (elementsInLine == width - 2)
                {
                    for (int l = i; l > 1; l--)
                    {
                        for (int m = 0; m < width - 1; m++)
                        {
                            gameBoard[l, m] = gameBoard[l - 1, m];
                        }
                    }
                    Game.Score = Game.Difficulty*Game.Difficulty;
                }
            }
        }


        //nadpisanie calej tablicy specjalną tablicą po zakonczeniu rozgrywki
        public void FailBoard()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Override(i,j,2);
                }
            }
        }
    }
}
