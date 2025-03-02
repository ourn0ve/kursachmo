using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SnakeGame1
{
    public class Snake
    {
        private List<Point> body;
        private Direction direction;

        public List<Point> Body => body;

        public Snake(Point initialPosition)
        {
            body = new List<Point> { initialPosition };
            direction = Direction.Right;
        }

        public void SetDirection(Direction newDirection)
        {
            if ((newDirection == Direction.Up && direction != Direction.Down) ||
                (newDirection == Direction.Down && direction != Direction.Up) ||
                (newDirection == Direction.Left && direction != Direction.Right) ||
                (newDirection == Direction.Right && direction != Direction.Left))
            {
                direction = newDirection;
            }
        }

        public void Move()
        {
            Point newHead = GetNextHeadPosition();
            body.Insert(0, newHead);
            body.RemoveAt(body.Count - 1);
        }

        public void Grow()
        {
            body.Add(body[body.Count - 1]);
        }

        public bool CheckCollisionWithSelf()
        {
            return body.Skip(1).Any(part => part == body[0]);
        }

        private Point GetNextHeadPosition()
        {
            if (body.Count == 0)
            {
                throw new InvalidOperationException("Snake body is empty.");
            }

            Point head = body[0];
            return direction switch
            {
                Direction.Up => new Point(head.X, head.Y - 1),
                Direction.Down => new Point(head.X, head.Y + 1),
                Direction.Left => new Point(head.X - 1, head.Y),
                Direction.Right => new Point(head.X + 1, head.Y),
                _ => head,
            };
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}