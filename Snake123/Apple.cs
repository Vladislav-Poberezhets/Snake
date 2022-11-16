using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Apple
    {
        public Position Position { get; set; }

        public Apple(Position position) => Position = position;
    }

    public static class SomeOtherAppleExtentions
    {
        public static Apple CreateApple(this Apple apple)
        {
            var rows = 20;
            var columns = 20;
            var random = new Random();
            var top = random.Next(0, rows + 1);
            var left = random.Next(0, columns + 1);
            apple = new Apple(new Position(top, left));
            return apple;
        }

        public static void Render(this Apple apple)
        {
            Console.SetCursorPosition(apple.Position.Left, apple.Position.Top);
            Console.Write("*");
        }
    }
}
