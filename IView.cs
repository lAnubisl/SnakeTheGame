namespace SnakeTheGame
{
    public interface IView
    {
        void ReflectGameChanges();

        void SetSnake(ISnake snake);
    }
}
