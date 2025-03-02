using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnakeGame1
{
    public class Obstacle
    {
        public Point Position { get; }

        public Obstacle(Point position)
        {
            Position = position;
        }
    }

}
