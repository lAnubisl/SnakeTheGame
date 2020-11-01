using System;
using System.Collections.Generic;

namespace SnakeTheGame
{

    public class Snake : ISnake
    {
        private Direction direction = Direction.Up;
        private IGameField gameField;
        private readonly LinkedList<Coordinate> snake = new LinkedList<Coordinate>();

        public Snake(int x, int y)
        {
            snake.AddFirst(new LinkedListNode<Coordinate>(new Coordinate(x, y)));
            Changes.Enqueue(new SnakeCoordinateChange(snake.First.Value, SnakeCoordinateChangeType.Add));
        }

        public void SetGameField(IGameField gameField)
        {
            this.gameField = gameField;
        }

        public void ChangeDirection(Direction direction)
        {
            this.direction = direction;
        }

        public void Move()
        {
            var newHead = GetNewHeadCoordinate();
            snake.AddBefore(snake.First, newHead);
            Changes.Enqueue(new SnakeCoordinateChange(newHead, SnakeCoordinateChangeType.Add));

            if (gameField.GetDotType(newHead) == DotType.Fruit)
            {
                return;
            }

            Changes.Enqueue(new SnakeCoordinateChange(snake.Last.Value, SnakeCoordinateChangeType.Remove));
            snake.RemoveLast();
        }

        private Coordinate GetNewHeadCoordinate()
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Coordinate(snake.First.Value.x, snake.First.Value.y - 1);
                case Direction.Down:
                    return new Coordinate(snake.First.Value.x, snake.First.Value.y + 1);
                case Direction.Left:
                    return new Coordinate(snake.First.Value.x - 1, snake.First.Value.y);
                case Direction.Right:
                    return new Coordinate(snake.First.Value.x + 1, snake.First.Value.y);
                default:
                    throw new NotImplementedException();
            }
        }

        public readonly Queue<SnakeCoordinateChange> Changes = new Queue<SnakeCoordinateChange>();
    }
}
