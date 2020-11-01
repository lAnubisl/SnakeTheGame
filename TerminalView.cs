using System;
using System.Collections.Generic;

namespace SnakeTheGame
{
    public class TerminalView : IView
    {
        private readonly Queue<GameChangeItem> gameChangeItems;
        private ISnake snake;

        public TerminalView(Queue<GameChangeItem> gameChangeItems, int width, int height)
        {
            this.gameChangeItems = gameChangeItems;
            Console.WindowHeight = height;
            Console.WindowWidth = width;
            Console.CursorVisible = false;
        }

        public void StartReadingKeys()
        {
            while (true)
            {
                var keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow: snake.ChangeDirection(Direction.Left); break;
                    case ConsoleKey.RightArrow: snake.ChangeDirection(Direction.Right); break;
                    case ConsoleKey.UpArrow: snake.ChangeDirection(Direction.Up); break;
                    case ConsoleKey.DownArrow: snake.ChangeDirection(Direction.Down); break;
                }
            }
        }

        public void ReflectGameChanges()
        {
            while(gameChangeItems.TryDequeue(out GameChangeItem change))
            {
                Console.SetCursorPosition(change.Coordinate.x, change.Coordinate.y);

                if (change.GameChangeType == GameChangeType.WallAppear)
                {
                    Console.Write("#");
                }

                if (change.GameChangeType == GameChangeType.WallDisappear
                 || change.GameChangeType == GameChangeType.SnakeDisappear)
                {
                    Console.Write(" ");
                }

                if (change.GameChangeType == GameChangeType.SnakeAppear)
                {
                    Console.Write("@");
                }

                if (change.GameChangeType == GameChangeType.FruitAppear)
                {
                    Console.Write("%");
                }
            }
        }

        public void SetSnake(ISnake snake)
        {
            this.snake = snake;
        }
    }
}
