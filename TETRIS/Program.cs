using System;

namespace TETRIS
{

    class Program
    {
        static void Main()
        {
            Console.SetWindowSize(Console.LargestWindowWidth / 3, Console.LargestWindowHeight/2);
            Console.CursorVisible = false;

            //deklaracje obiektów uzywanych w grze
            ConsoleKeyInfo Key = new ConsoleKeyInfo();          

            Board GameBoard = new Board(12, 24);    //(SZEROKOSC,WYSOKOSC) kształt pola jest modularny
            
            Game Game = new Game(500);          //(OPOZNIENIE STARTOWE), 500 zalecane

            Random Rand = new Random((int)DateTime.Now.Ticks);

            Tetromino Block;


            int BlockType, Color;       //pomocnicze tymczasowe zmienne
            GameBoard.Draw();     
            Game.ShowStats(GameBoard);      //wyswietlenie podstawowych elementów gry
            Game.Start(GameBoard);
            int timePassed = 0;
            int blocksSaved = 0;
            int startDelay = Game.Delay;


            BlockType = Rand.Next(0, 7);        //losowanie koloru i typu następnego bloczka
            Color = Rand.Next(2, 8);
            switch (BlockType)          //Polimorfizm abstrakcyjnego typu Tetromino w konkretny typ dziedziczony
            {
                case 0:
                    {
                        Block = new SquareShape((GameBoard.Width-2) / 2, 0, Color);
                        break;
                    }
                case 1:
                    {
                        Block = new TShape((GameBoard.Width-2) / 2, 0, Color);
                        break;
                    }
                case 2:
                    {
                        Block = new ZShape((GameBoard.Width-2) / 2, 0, Color);
                        break;
                    }
                case 3:
                    {
                        Block = new SShape((GameBoard.Width-2) / 2, 0, Color);
                        break;
                    }
                case 4:
                    {
                        Block = new IShape((GameBoard.Width-2) / 2, 0, Color);
                        break;
                    }
                case 5:
                    {
                        Block = new RLShape((GameBoard.Width-2) / 2, 0, Color);
                        break;
                    }
                case 6:
                    {
                        Block = new LShape((GameBoard.Width-2) / 2, 0, Color);
                        break;
                    }
                default:
                    {
                        Block = new SquareShape((GameBoard.Width-2) / 2, 0, Color);
                        break;
                    }
            }


            while (Game.GameState == true)      //Głowna pętla gry
            {
                GameBoard.Draw();
                Block.Draw();               //wyswietlenie utworzonego kształtu
                Game.ShowStats(GameBoard);

                if (Console.KeyAvailable)       //zbieranie inputów
                {
                    Key = Console.ReadKey();
                    Block.Move(Key.Key, GameBoard,Game);
                    Game.ClearInputBuffer();
                }

                System.Threading.Thread.Sleep(50);      //opóźnienie
                timePassed += 50;   //pamiętanie czasu który minął

                if (timePassed >= Game.Delay)
                {
                    timePassed = 0;
                    Block.MoveDown(GameBoard);      //opuszczenie bloczka po upłynięciu czasu
                }

                if (Block.BottomCollision(GameBoard))   //kolizja z planszą
                {
                    Block.SaveBlock(GameBoard);
                    GameBoard.RemoveLines(Game);
                    Block.ReturnToStart(GameBoard);               
                    Game.ModifySpeed(startDelay);
                    blocksSaved++;
                    if(blocksSaved % 10 == 0)       //modyfikacja trudności raz na 10 bloczków
                    {
                        Game.Difficulty++;
                    }
                    BlockType = Rand.Next(0, 7);    //generowanie następnego bloczka
                    Color = Rand.Next(2, 8);
                    switch (BlockType)
                    {
                        case 0:
                            {
                                Block = new SquareShape((GameBoard.Width - 2) / 2, 0, Color);
                                break;
                            }
                        case 1:
                            {
                                Block = new TShape((GameBoard.Width - 2) / 2, 0, Color);
                                break;
                            }
                        case 2:
                            {
                                Block = new ZShape((GameBoard.Width - 2) / 2, 0, Color);
                                break;
                            }
                        case 3:
                            {
                                Block = new SShape((GameBoard.Width - 2) / 2, 0, Color);
                                break;
                            }
                        case 4:
                            {
                                Block = new IShape((GameBoard.Width - 2) / 2, 0, Color);
                                break;
                            }
                        case 5:
                            {
                                Block = new RLShape((GameBoard.Width - 2) / 2, 0, Color);
                                break;
                            }
                        case 6:
                            {
                                Block = new LShape((GameBoard.Width - 2) / 2, 0, Color);
                                break;
                            }
                        default:
                            {
                                Block = new SquareShape((GameBoard.Width - 2) / 2, 0, Color);
                                break;
                            }
                    }
                    if (Block.BottomCollision(GameBoard))   //warunek porażki
                    {
                        Game.GameState = false;
                    }
                }
            }
            Console.Clear();        //ekran końcowy
            Game.ShowStats(GameBoard);
            GameBoard.FailBoard();
            GameBoard.Draw();
            Game.GameOver(GameBoard);
        }
    }
}
