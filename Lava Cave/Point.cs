using System;
using System.Collections.Generic;
using System.Text;

namespace Lava_Cave
{
    class Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        public void add(Point other)
        {

            this.x = this.x + other.x;
            this.y = this.y + other.y;
        }

    }
}
