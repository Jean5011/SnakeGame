using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Juego
{
    internal class Program
    {
        static int width = 50;
        static int height = 20;
        static int score = 0;
        static bool gameOver = false;
        static Random random = new Random();

        static int snakeX;
        static int snakeY;
        static int fruitX;
        static int fruitY;
        static int[] tailX = new int[1000];
        static int[] tailY = new int[1000];
        static int tailLength = 0;
        static int speed = 10;
        static int direction = 0;

        static bool juego= true;


        static void Main(string[] args)
        {
            Console.Title = "Snake Game";
            Console.CursorVisible = false;

            InitializeGame();

            while (!gameOver)
            {
                if (Console.KeyAvailable)
                {
                    HandleKeypress(Console.ReadKey(true).Key);
                }

                MoveSnake();

                if (CheckCollision())
                {
                    gameOver = true;
                }

                Draw();
                Thread.Sleep(1000 / speed);
            }
            Console.Clear();
            Console.SetCursorPosition(width / 2 - 5, height / 2);
            Console.WriteLine("Game Over! Tu puntaje es de: " + score + ".");
            Console.ReadLine();
        }

        static void InitializeGame()
        {
            snakeX = width / 2;
            snakeY = height / 2;

            fruitX = random.Next(1, width - 1);
            fruitY = random.Next(1, height - 1);

            score = 0;

            direction = 0;
        }

        static void Draw()
        {
            Console.Clear();

            for (int i = 0; i < width + 2; i++)
            {
                Console.Write(".");
            }
            Console.WriteLine();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (j == 0)
                    {
                        Console.Write(".");
                    }
                    else if (j == width - 1)
                    {
                        Console.Write(".");
                    }
                    else if (i == snakeY && j == snakeX)
                    {
                        Console.Write("+");
                    }
                    else if (i == fruitY && j == fruitX)
                    {
                        Console.Write("ó");
                    }
                    else
                    {
                        bool tailBit = false;
                        for (int k = 0; k < tailLength; k++)
                        {
                            if (tailX[k] == j && tailY[k] == i)
                            {
                                tailBit = true;
                                break;
                            }
                        }

                        if (tailBit)
                        {
                            Console.Write("+");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                }
                Console.WriteLine();
            }

            for (int i = 0; i < width + 2; i++)
            {
                Console.Write(".");
            }

            Console.WriteLine();
            Console.WriteLine("Score: " + score);
        }

        static void HandleKeypress(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if (direction != 2)
                    {
                        direction = 0;
                    }
                    break;

                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    if (direction != 3)
                    {
                        direction = 1;
                    }
                    break;

                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if (direction != 0)
                    {
                        direction = 2;
                    }
                    break;

                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if (direction != 1)
                    {
                        direction = 3;
                    }
                    break;

                case ConsoleKey.Escape:
                    gameOver = true;
                    break;
            }
        }

        static void MoveSnake()
        {
            int prevX = tailX[0];
            int prevY = tailY[0];
            int prev2X, prev2Y;
            tailX[0] = snakeX;
            tailY[0] = snakeY;

            for (int i = 1; i < tailLength; i++)
            {
                prev2X = tailX[i];
                prev2Y = tailY[i];
                tailX[i] = prevX;
                tailY[i] = prevY;
                prevX = prev2X;
                prevY = prev2Y;
            }

            switch (direction)
            {
                case 0:
                    snakeY--;
                    break;

                case 1:
                    snakeX++;
                    break;

                case 2:
                    snakeY++;
                    break;

                case 3:
                    snakeX--;
                    break;
            }

            if (snakeX == 0 || snakeX == width - 1 || snakeY == 0 || snakeY == height)
            {
                gameOver = true;
            }

            if (snakeX == fruitX && snakeY == fruitY)
            {
                score += 10;
                tailLength++;
                fruitX = random.Next(1, width - 1);
                fruitY = random.Next(1, height - 1);
            }
        }

        static bool CheckCollision()
        {
            for (int i = 0; i < tailLength; i++)
            {
                if (tailX[i] == snakeX && tailY[i] == snakeY)
                {
                    return true;
                }
            }
            return false;

        }

        static bool TeclaJuego(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.S:
                    return true;
                    break;
                default: return false;
                    break;
            }
        }

    }
}
