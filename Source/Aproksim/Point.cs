using System;

namespace Approximation
{
    public class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double X { get; set; }
        public double Y { get; set; }

        public static implicit operator System.Drawing.PointF(Point p)  
        {
            return new System.Drawing.PointF()
            {
                X = (float)p.X,
                Y = (float)p.Y
            };
        }
    }
}