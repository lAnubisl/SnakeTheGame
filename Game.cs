using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeTheGame
{
    public class Game : IGameField
    {
        public readonly int width, height;
        private readonly DotType[,] gameField;
        private Snake snake;
        public readonly Queue<GameChangeItem> GameChanges = new Queue<GameChangeItem>();
        private IView view;

        public Game(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.gameField = new DotType[width, height];
            InitGameField();
        }

        public void SetView(IView view)
        {
            this.view = view;
            this.view.SetSnake(this.snake);
            Task.Factory.StartNew(() => {
                while (true)
                {
                    snake.Move();
                    ReflectSnake();
                    view.ReflectGameChanges();
                    Thread.Sleep(300);
                }
            });
        }

        private void InitGameField()
        {
            InitPerimeter();
            InitSnake();
            InitFruits();
        }

        private void InitSnake()
        {
            snake = new Snake(width / 2, height / 2);
            snake.SetGameField(this);
            ReflectSnake();
        }

        private void InitFruits()
        {
            var random = new Random();
            for(int i = 1; i <= 10; i++)
            {
                var x = random.Next(1, width - 1);
                var y = random.Next(1, height - 1);
                if (gameField[x, y] != DotType.FreeSpace)
                {
                    i--;
                    continue;
                }

                gameField[x, y] = DotType.Fruit;
                GameChanges.Enqueue(new GameChangeItem(GameChangeType.FruitAppear, new Coordinate(x, y)));
            }
        }

        private void ReflectSnake()
        {
            while(snake.Changes.TryDequeue(out SnakeCoordinateChange change))
            {
                if (change.SnakeCoordinateChangeType == SnakeCoordinateChangeType.Add)
                {
                    if (gameField[change.SnakeCoordinate.x, change.SnakeCoordinate.y] == DotType.Wall)
                    {
                        GameOver();
                    }

                    gameField[change.SnakeCoordinate.x, change.SnakeCoordinate.y] = DotType.Snake;
                    GameChanges.Enqueue(new GameChangeItem(GameChangeType.SnakeAppear, change.SnakeCoordinate));
                    continue;
                }

                if (change.SnakeCoordinateChangeType == SnakeCoordinateChangeType.Remove)
                {
                    gameField[change.SnakeCoordinate.x, change.SnakeCoordinate.y] = DotType.FreeSpace;
                    GameChanges.Enqueue(new GameChangeItem(GameChangeType.SnakeDisappear, change.SnakeCoordinate));
                    continue;
                }

                throw new NotImplementedException();
            }
        }

        private void GameOver()
        {
            throw new GameOverException();
        }

        private void InitPerimeter()
        {
            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                gameField[i, 0] = DotType.Wall;
                GameChanges.Enqueue(new GameChangeItem(GameChangeType.WallAppear, new Coordinate(i, 0)));
            }

            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                gameField[i, gameField.GetLength(1) - 1] = DotType.Wall;
                GameChanges.Enqueue(new GameChangeItem(GameChangeType.WallAppear, new Coordinate(i, gameField.GetLength(1) - 1)));
            }

            for (int i = 0; i < gameField.GetLength(1); i++)
            {
                gameField[0, i] = DotType.Wall;
                GameChanges.Enqueue(new GameChangeItem(GameChangeType.WallAppear, new Coordinate(0, i)));
            }

            for (int i = 0; i < gameField.GetLength(1); i++)
            {
                gameField[gameField.GetLength(0) - 1, i] = DotType.Wall;
                GameChanges.Enqueue(new GameChangeItem(GameChangeType.WallAppear, new Coordinate(gameField.GetLength(0) - 1, i)));
            }
        }

        public DotType GetDotType(Coordinate coordinate)
        {
            return this.gameField[coordinate.x, coordinate.y];
        }
    }
}
