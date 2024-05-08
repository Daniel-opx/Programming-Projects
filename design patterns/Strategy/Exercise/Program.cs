using System.Numerics;

namespace Exercise
{
    public interface IDiscriminantStrategy
    {
        double CalculateDiscriminant(double a, double b, double c);
    }

    public class OrdinaryDiscriminantStrategy : IDiscriminantStrategy
    {
        // todo
        public double CalculateDiscriminant(double a, double b, double c) => (Math.Pow(b, 2)) - (4 * a * c);
       
    }

    public class RealDiscriminantStrategy : IDiscriminantStrategy
    {
        // todo (return NaN on negative discriminant!)
        public double CalculateDiscriminant(double a, double b, double c)
        {
            var disriminat = (Math.Pow(b, 2)) - (4 * a * c);
            return disriminat > 0 ? disriminat : double.NaN;
        }
    }

    public class QuadraticEquationSolver
    {
        private readonly IDiscriminantStrategy strategy;

        public QuadraticEquationSolver(IDiscriminantStrategy strategy)
        {
            this.strategy = strategy;
        }

        public Tuple<Complex, Complex> Solve(double a, double b, double c)
        {
            var discriminant = strategy.CalculateDiscriminant(a, b, c);
            var plusSolve = (-1 * b + Math.Sqrt (discriminant))/2*a;
            var minusSolve = (-1 * b - Math.Sqrt(discriminant)) / 2 * a;
            if(strategy is RealDiscriminantStrategy)
            {
                if(discriminant < 0) return new Tuple<Complex, Complex>(Complex.NaN, Complex.NaN);
            }
            return new Tuple<Complex,Complex> (plusSolve, minusSolve);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
