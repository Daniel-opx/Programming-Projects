using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_6
{
    internal class Parabola
    {
        public SortedList<double, Coordinate> coordinateslist;

        internal double coefficientA;
        internal double coefficientB;
        public Parabola()
        {
            coordinateslist = new SortedList<double, Coordinate>();

        }


        internal void AddCoordinate(double x, double y)
        {
            coordinateslist.Add(x, new Coordinate(x, y));
        }



        static internal ParabolaCoefficient FindCoefficient(Coordinate c1, Coordinate c2) 
        {

            // Slope calculation for the line passing through each point and the origin
            double m1 = c1.Y / c1.X; // Slope for point 1
            double m2 = c2.Y / c2.X; // Slope for point 2

            // Coefficient 'a' calculation (using the slope)
            double a = (m2 - m1) / (c2.X - c1.X);

            // Coefficient 'b' calculation (using one of the points)
            double b = c1.Y - a * c1.X * c1.X;

            return new ParabolaCoefficient() { a = a, b = b };
        }

       public static double[] SolveQuadraticInequality(double a, double b, double d)
        {
            double[] solutions;
            d = d * -1;
            // Solve the inequality
            double discriminant = Math.Pow(b, 2) - 4 * a * d;

            if (a == 0)
            {
                if (b > 0)
                    return new double[] { d / b, double.PositiveInfinity };
                else if (b < 0)
                    return new double[] { double.NegativeInfinity, d / b };
                else
                    return new double[0]; // No solution exists
            }
            else if (discriminant < 0)
            {
                return new double[0]; // No real solutions exist
            }
            else
            {
                double x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                double x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                if(!GeneralStaticMethods.isNatural(x1)) x1 = Math.Floor(x1);
                
                if (x1 > x2)
                {
                    solutions = new double[] { x2, x1 };
                }
                else
                {
                    solutions = new double[] { x1, x2 };
                }
            }

            return solutions;


        }
    }
}
