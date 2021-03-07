using System;

namespace TETRIS
{

    class Program
    {
        static void ClearBuffer()
        {
            if (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
        }
        static void Main(string[] args)
        {
            ConsoleKeyInfo Key = new ConsoleKeyInfo();
            Console.CursorVisible = false;
            Board GameBoard = new Board(10, 22);
            GameBoard.Draw();
            Tetromino Block;
            Random Rand = new Random((int)DateTime.Now.Ticks);
            int r;
            r = Rand.Next(0, 7);
            switch (r)
            {
                case 0:
                    {
                        Block = new SquareShape(GameBoard.Width / 2, 0, 1);
                        break;
                    }
                case 1:
                    {
                        Block = new TShape(GameBoard.Width / 2, 0, 1);
                        break;
                    }
                case 2:
                    {
                        Block = new ZShape(GameBoard.Width / 2, 0, 1);
                        break;
                    }
                case 3:
                    {
                        Block = new SShape(GameBoard.Width / 2, 0, 1);
                        break;
                    }
                case 4:
                    {
                        Block = new IShape(GameBoard.Width / 2, 0, 1);
                        break;
                    }
                case 5:
                    {
                        Block = new RLShape(GameBoard.Width / 2, 0, 1);
                        break;
                    }
                case 6:
                    {
                        Block = new LShape(GameBoard.Width / 2, 0, 1);
                        break;
                    }
                default:
                    {
                        Block = new SquareShape(GameBoard.Width / 2, 0, 1);
                        break;
                    }
            }
            int timePassed = 0;
            while (true)
            {
                GameBoard.Draw();
                Block.Draw();
                if (Console.KeyAvailable)
                {
                    Key = Console.ReadKey();
                    Block.Move(Key.Key, GameBoard);
                    ClearBuffer();
                }

                System.Threading.Thread.Sleep(100);
                timePassed += 100;

                if (timePassed >= 200)
                {
                    timePassed = 0;
                    Block.MoveDown(GameBoard);
                }
                if (Block.BottomCollision(GameBoard))
                {
                    Block.SaveBlock(GameBoard);
                    GameBoard.RemoveLines();
                    Block.ReturnToStart();
                    r = Rand.Next(0, 7);
                    switch (r)
                    {
                        case 0:
                            {
                                Block = new SquareShape(GameBoard.Width / 2, 0, 1);
                                break;
                            }
                        case 1:
                            {
                                Block = new TShape(GameBoard.Width / 2, 0, 1);
                                break;
                            }
                        case 2:
                            {
                                Block = new ZShape(GameBoard.Width / 2, 0, 1);
                                break;
                            }
                        case 3:
                            {
                                Block = new SShape(GameBoard.Width / 2, 0, 1);
                                break;
                            }
                        case 4:
                            {
                                Block = new IShape(GameBoard.Width / 2, 0, 1);
                                break;
                            }
                        case 5:
                            {
                                Block = new RLShape(GameBoard.Width / 2, 0, 1);
                                break;
                            }
                        case 6:
                            {
                                Block = new LShape(GameBoard.Width / 2, 0, 1);
                                break;
                            }
                        default:
                            {
                                Block = new SquareShape(GameBoard.Width / 2, 0, 1);
                                break;
                            }
                    }
                }
            }
        }
    }
}
