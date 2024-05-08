namespace Dynamic_Decorator_Composition
{
    public interface IShape
    {
         string AsString();
    }
    public class Circle : IShape
    {
        private float radius;

        public Circle(float radius)
        {
            this.radius = radius;
        }

        public string AsString() => $"A Circle with radius {radius}";
        
        public void resize(float factor)
        {
            radius *= factor;
        }
        
    }
    public class Square : IShape
    {
        private float side;

        public Square(float side)
        {
            this.side = side;
        }
       
        public string AsString() => $"A square with side {side}";
    }

    public class ColoredShape : IShape
    {
        private IShape shape;
        private string color;

        public ColoredShape(IShape shape, string color)
        {
            this.shape = shape ?? throw new ArgumentNullException(nameof(shape));
            this.color = color ?? throw new ArgumentNullException(nameof(color));
        }


        public string AsString()
        {
            return $"{shape.AsString()} has the color of {color}";
        }
    }
    public class TransparentShape : IShape
    {
        private IShape shape;
        private float transperency;

        public TransparentShape(IShape shape, float transperency)
        {
            this.shape = shape;
            this.transperency = transperency;
        }

        public string AsString() => $"{shape.AsString()} has transperency of {transperency * 100.0}%";
        
    }
    internal class DyanmicDC
    {
        static void Main(string[] args)
        {
            var square = new Square(5);
            var circle = new Circle(3);
            var redsquare = new ColoredShape(square,"red");
            Console.WriteLine($"{redsquare.AsString()}");
            var halfTransperentSquare = new TransparentShape(redsquare, 50);
            Console.WriteLine($"{halfTransperentSquare.AsString()}");


        }
    }
}
