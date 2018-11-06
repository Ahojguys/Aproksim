using System;

namespace Approximation
{
    public class RunLib
    {
        public static double[] Calculate(Point[] points)
        {            
            Approximator approximator = new Approximator();
            return approximator.Approximate(points);            
        }

    }
}