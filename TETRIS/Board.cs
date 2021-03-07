using System;
using System.Collections.Generic;
using System.Text;

namespace TETRIS
{
    public class Board
    {
        private int width;
        private int height;
        private int[,] gameBoard;

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
            }
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    switch (gameBoard[i,j])
                    {
                        case 0:
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
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
                    }
                }
                Console.Write("\n");
            }
        }
        public void Override(int y, int x, int value)
        {
            gameBoard[y, x] = value;
        }

        public void RemoveLines()
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
                }
            }

        }
    }
}
