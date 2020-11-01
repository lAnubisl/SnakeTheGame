namespace SnakeTheGame
{
    public class SnakeCoordinateChange
    {
        public SnakeCoordinateChange(Coordinate snakeCoordinate, SnakeCoordinateChangeType snakeCoordinateChangeType)
        {
            this.SnakeCoordinate = snakeCoordinate;
            this.SnakeCoordinateChangeType = snakeCoordinateChangeType;
        }
        public Coordinate SnakeCoordinate { get; }
        public SnakeCoordinateChangeType SnakeCoordinateChangeType { get; }
    }
}
