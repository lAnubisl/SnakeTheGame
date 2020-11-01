namespace SnakeTheGame
{
    public class GameChangeItem
    {
        public GameChangeItem(GameChangeType gameChangeType, Coordinate coordinate)
        {
            GameChangeType = gameChangeType;
            Coordinate = coordinate;
        }

        public Coordinate Coordinate { get; }
        public GameChangeType GameChangeType { get; }
    }
}
