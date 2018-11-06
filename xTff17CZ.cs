using System;

namespace Approximation
{
    public class Approximator
    {
        /// <summary>
        /// Аппроксимирует точки с помощью метода наименьших квадратов
        /// </summary>
        /// <param name="points">точки с координатами X,Y</param>
        /// <returns>Возвращает массив коээфициентов аппроксимирующей линии</returns>
        public double[] Approximate(Point[] points)
        {
            double n = points.Length;
            // для того чтобы посчитать коэффициенты a и b, нужно посчитать набор сумм
            // сумму всех X
            double xSum = getXSum(points);
            // сумму всех Y
            double ySum = getYSum(points);
            // сумму всех XY 
            double xySum = getXYSum(points);
            // сумму всех X^2
            double xSquareSum = getXSquareSum(points);
            
            // вычисляем a - первый коэффициент
            double a = (n * xySum - xSum * ySum) / (n*xSquareSum-xSum*xSum);
            // вычисляем b - второй коэффициент
            double b = (ySum - a * xSum) / n;
            
            double[] coefficients = new double[2];
            coefficients[0] = a;
            coefficients[1] = b;
            return coefficients;
        }

        private double getXYSum(Point[] points)
        {
            double sum = 0;
            for (int i = 0; i < points.Length; i++)
            {
                sum += points[i].X * points[i].Y;
            }

            return sum;
        }

        private double getXSquareSum(Point[] points)
        {
            double sum = 0;
            for (int i = 0; i < points.Length; i++)
            {
                sum += points[i].X * points[i].X;
            }

            return sum;
        }

        private double getYSum(Point[] points)
        {
            double sum = 0;
            for (int i = 0; i < points.Length; i++)
            {
                sum += points[i].Y;
            }

            return sum;
        }

        private double getXSum(Point[] points)
        {
            double sum = 0;
            for (int i = 0; i < points.Length; i++)
            {
                sum += points[i].X;
            }

            return sum;
        }
    }
}