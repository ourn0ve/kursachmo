using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SnakeGame1
{
    public class GameField
    {
        public int Width { get; }
        public int Height { get; }
        public List<Obstacle> Obstacles { get; }
        public Food Food { get; set; }

        private Random random = new Random();

        public GameField(int width, int height, bool isHardMode)
        {
            Width = width;
            Height = height;
            Obstacles = new List<Obstacle>();

            if (isHardMode)
            {
                GenerateObstacles();
            }

            Food = GenerateFood();
        }

        public Food GenerateFood()
        {
            Position foodPosition;
            do
            {
                int x = random.Next(0, Width);  // Используем Width
                int y = random.Next(0, Height); // Используем Height
                foodPosition = new Position(x, y);
            } while (Obstacles.Any(obs => obs.Position.X == foodPosition.X && obs.Position.Y == foodPosition.Y));

            return new Food(foodPosition);
        }

        private void GenerateObstacles()
        {
            for (int i = 0; i < 10; i++)
            {
                Obstacles.Add(new Obstacle(new Point(random.Next(Width), random.Next(Height))));
            }
        }

        public bool CheckCollision(Snake snake)
        {
            Point head = snake.Body[0];

            // Проверка на столкновение с препятствиями
            if (Obstacles.Any(obs => obs.Position.X == head.X && obs.Position.Y == head.Y))
            {
                return true;
            }

            // Проверка на столкновение с границами поля
            if (head.X < 0 || head.X >= Width || head.Y < 0 || head.Y >= Height)
            {
                return true;
            }

            // Проверка на столкновение с собой
            return snake.CheckCollisionWithSelf();
        }

        public bool CheckFoodCollision(Snake snake)
        {
            Point head = snake.Body[0];
            if (head.X == Food.Position.X && head.Y == Food.Position.Y)
            {
                snake.Grow();
                Food = GenerateFood();
                return true;
            }
            return false;
        }
    }

    public class Food
    {
        public Position Position { get; }

        public Food(Position position)
        {
            Position = position;
        }
    }

    public struct Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }


}