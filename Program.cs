using System;
using System.Threading;

namespace SnakeTheGame
{
    partial class Program
    {
        static void Main(string[] args)
        {
            int height = Console.LargestWindowHeight;
            int width = Console.LargestWindowWidth;

            Game game = new Game((int)(width * 0.7), (int)(height * 0.7));
            TerminalView view = new TerminalView(game.GameChanges, width, height);
            game.SetView(view);
            view.StartReadingKeys();
        }
    }
}
