using System;

namespace Approximation
{
    class Program
    {
        static void Main(string[] args)
        {
            Point[] points = new[]
            {
                new Point(2, 2),
                new Point(3, 5),
                new Point(6, 7.15),
                new Point(22, 53.2),
                new Point(0, 0.221)
            };
            
            Approximator approximator = new Approximator();
            double[] coeffs = approximator.Approximate(points);
            
            double a = coeffs[0];
            double b = coeffs[1];
            Console.WriteLine("Аппроксимирующая прямая y(x) ={0}*x + {1}",a,b);
            
        }

    }
}