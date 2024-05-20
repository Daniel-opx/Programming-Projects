namespace Factory_Method
{

   

    public class Point
    {
        //factory method


        private double x, y;
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }
            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            var point = Point.Factory.NewCartesianPoint(1, 5);
        }
    }
}
