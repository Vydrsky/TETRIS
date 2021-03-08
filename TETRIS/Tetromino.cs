using System;
using System.Collections.Generic;
using System.Text;

namespace TETRIS
{
    public abstract class Tetromino     //abstrakcyjna klasa opisująca każda możliwą funkcjonalność bloczka oprócz jego kształtu
    {
        protected int posX;
        protected int posY;
        protected int width;
        protected int height;
        protected int[,] blockArray;
        protected int color;
        

        //gettery i settery
        public int Width 
        {
            get
            {
                return width;
            }
            set
            {
                if (value <2)
                {
                    value = 2;
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
                if (value < 2)
                {
                    value = 2;
                }
                height = value;
            }
        }
        public int PosX
        {
            get
            {
                return posX;
            }
            set
            {
                if (posX < 0)
                {
                    posX = 0;
                }
                posX = value;
            }
        }
        public int PosY
        {
            get
            {
                return posY;
            }
            set
            {
                if (posY < 0)
                {
                    posY = 0;
                }
                posY = value;
            }
        }
        public int Color
        {
            set
            {
                Random Rnd = new Random((int)DateTime.Now.Ticks);
                color = Rnd.Next(2, 8);
            }
        }
        public Tetromino(int x,int y)
        {
            PosX = x*2;
            PosY = y;
        }

        //metoda służąca do wyświetlania bloczka
        public void Draw()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (blockArray[i, j] != 0)
                    {
                        Console.SetCursorPosition(j*2 + posX, i + posY);
                        switch (blockArray[i, j])
                        {
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
                }
                Console.Write("\n");
            }
        }


        //Metody sprawdzające różne wersje kolizji
        public bool BottomCollision(Board Board)
        {
            int[,] boardCopy = Board.GameBoard;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (blockArray[i, j] != 0 && boardCopy[i+posY+1,j+posX/2] !=0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool LeftCollision(Board Board)
        {
            int[,] boardCopy = Board.GameBoard;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    
                    if (blockArray[i,j] != 0 && boardCopy[i + posY, j + posX / 2 - 1] != 0)
                    {
                        return true;
                    }
                    
                }
            }

            return false;
        }

        public bool RightCollision(Board Board)
        {
            int[,] boardCopy = Board.GameBoard;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    if (blockArray[i, j] != 0 && boardCopy[i + posY, j + posX / 2 + 1] != 0)
                    {
                        return true;
                    }

                }
            }

            return false;
        }

        public virtual bool InternalCollision(Board Board)
        {
            int[,] boardCopy = Board.GameBoard;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    if (blockArray[i, j] != 0 && boardCopy[i + posY, j + posX / 2] != 0)
                    {
                        return true;
                    }

                }
            }

            return false;
        }


        //metoda służąca do przemieszczania bloczka pod wpływem inputu od strony użytkownika
        public void Move(ConsoleKey Key, Board Board,Game Game)
        {
            if (Key == ConsoleKey.LeftArrow && !LeftCollision(Board))
            {
                PosX-=2;
            }
            if (Key == ConsoleKey.RightArrow && !RightCollision(Board))
            {
                PosX+=2;
            }
            if (Key == ConsoleKey.UpArrow)
            {
                Rotate(Board);
            }
            if (Key == ConsoleKey.DownArrow)
            {
                Game.Delay = 50;
            }
        }

        public void MoveDown(Board Board)
        {
            if (!BottomCollision(Board))
            {
                PosY++;
            }
        }

        //Metoda służąca do nadpisywania pola gry bloczkiem który wylądował
        public void SaveBlock(Board Board)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if(blockArray[i,j] != 0)
                    {
                        Board.Override(i+posY, j+posX/2, blockArray[i, j]);
                    }
                }
            }
        }

        //metoda powracająca bloczek na pozycje startową
        public void ReturnToStart(Board Board)
        {
            PosX = (Board.Width/2)-2;      
            PosY = 0;
        }


        //metoda obracająca tablicę bloczka, odpowiednio zmieniająca sposób indexowania
        public void Rotate(Board Board)
        {
            int[,] temp = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                int k = width - 1;
                for (int j = 0; j < width; j++)
                {
                    temp[i, k] = blockArray[j, i];
                    k--;
                }
            }
            for (int i = 0; i < height; i++)
            {
                int k = width - 1;
                for (int j = 0; j < width; j++)
                {
                    blockArray[i, j] = temp[i, j];
                }
            }
            while (InternalCollision(Board))        //zabezpieczenie przed nieprawidłowym obrotem, bloczek obraca się rekurencyjnie aż nie znajdzie pozycji, lub nie wróci na orgyinalną pozycje
            {
                Rotate(Board);
            }
        }

    }


    //Klasy potomne, opisują one dokładną formę bloczka, dziedziczą wszystkie cechy z klasy Tetromino
    public class TShape : Tetromino
    {
        public TShape(int x,int y,int c) : base(x, y)
        {
            color = c;
            Height = 3;
            Width = 3;
            blockArray = new int[3, 3] { {0,    color,0     },
                                         {color,color,color },
                                         {0,    0,    0 }   };
        }
    }

    public class SquareShape : Tetromino
    {
        public SquareShape(int x, int y, int c) : base(x, y)
        {
            color = c;
            Height = 2;
            Width = 2;
            blockArray = new int[2, 2] { {color,color },
                                         {color,color }};
        }
    }

    public class ZShape : Tetromino
    {
        public ZShape(int x, int y, int c) : base(x, y)
        {
            color = c;
            Height = 3;
            Width = 3;
            blockArray = new int[3, 3] { {color,color,0     },
                                         {0    ,color,color },
                                         {0,    0,    0 }   };
        }
    }

    public class SShape : Tetromino
    {
        public SShape(int x, int y, int c) : base(x, y)
        {
            color = c;
            Height = 3;
            Width = 3;
            blockArray = new int[3, 3] { {0,    color,color },
                                         {color,color,0     },
                                         {0,    0,    0 }   };
        }
    }

    public class IShape : Tetromino
    {
        public IShape(int x, int y, int c) : base(x, y)
        {
            color = c;
            Height = 4;
            Width = 4;
            blockArray = new int[4, 4] { { 0,color,0,0},
                                         { 0,color,0,0},
                                         { 0,color,0,0},
                                         { 0,color,0,0}};
        }

        public override bool InternalCollision(Board Board)     //Ze względu na większą tablice 4x4, aby uniknąć ujemnych indexów, metoda jest nadpisana
        {
            int[,] boardCopy = Board.GameBoard;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    while (PosX < 0)
                    {
                        PosX++;
                    }
                    if (blockArray[i, j] != 0 && boardCopy[i + posY, j + posX / 2] != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }

    public class LShape : Tetromino
    {
        public LShape(int x, int y, int c) : base(x, y)
        {
            color = c;
            Height = 3;
            Width = 3;
            blockArray = new int[3, 3] { {0,color,0    },
                                         {0,color,0    },
                                         {0,color,color}   };
        }
    }

    public class RLShape : Tetromino
    {
        public RLShape(int x, int y, int c) : base(x, y)
        {
            color = c;
            Height = 3;
            Width = 3;
            blockArray = new int[3, 3] { {0,color,color},
                                         {0,color,0    },
                                         {0,color,0}   };
        }
    }
}
