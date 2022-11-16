using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Game
    {
        public Position StartPosition = new(1, 0);
        public Direction CurrentDirection;
        public Direction NextDirection;
        public Snake _snake;
        public Apple _apple;
        private int _rate = 120;
        public TimeSpan GameRate => TimeSpan.FromMilliseconds(_rate);

        public Game()
        {
            _snake = new Snake(StartPosition, 3);
            _apple = _apple.CreateApple();
            CurrentDirection = Direction.Right;
            NextDirection = Direction.Right;
        }

        public bool IsGameOver => !_snake.IsAlive;

        public void OnKeyPress(ConsoleKey key)
        {
            Direction newDirection = CurrentDirection;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    newDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    newDirection = Direction.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    var test = _snake;
                    newDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    newDirection = Direction.Right;
                    break;
                case ConsoleKey.End:
                    _rate = _rate > 10 ? _rate - 10 : _rate;
                    break;
                case ConsoleKey.Delete:
                    _rate = _rate < 800 ? _rate + 10 : _rate;
                    break;
                default: return;
            }

            if (newDirection == CurrentDirection.OppositeDirection()) return;
            NextDirection = newDirection;
        }

        public void Render()
        {
            Console.Clear();
            _snake.Render(this);
            _apple.Render();
            Console.SetCursorPosition(0, 0);
        }

        public void OnTick()
        {
            if (IsGameOver) throw new Exception();
            CurrentDirection = NextDirection;
            _snake.Move(CurrentDirection);
            if (_snake.Head.Equals(_apple.Position))
            {
                _snake.Grow();
                _apple = _apple.CreateApple();
            }
        }
    }

    public static class GameExtensions
    {
        public static void RotateSnakeIfBounds(this Game game)
        {
            if (game._snake.Head.Left == Console.WindowWidth - 6 || game._snake.Head.Left == 0 ||
                game._snake.Head.Top == Console.WindowHeight - 6 || game._snake.Head.Top == 0)
                game.NextDirection = game.CurrentDirection.OppositeDirection();
        }
    }
}
