namespace Factory_Method
{

    public class PointFactory
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

    public class Point
    {
        //factory method
        

        private double x,y;
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
           
        }
    }
}
