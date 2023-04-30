using System;
using System.Threading;

namespace SnakeTheGame
{
    partial class Program
    {
        static void Main(string[] args)
        {
            int height = 40; //Console.LargestWindowHeight;
            int width = 60; //Console.LargestWindowWidth;

            Game game = new Game((int)(width * 0.7), (int)(height * 0.7));
            TerminalView view = new TerminalView(game.GameChanges, width, height);
            game.SetView(view);
            view.StartReadingKeys();
        }
    }
}
