namespace Dynamic_Decorator_Composition
{
    public abstract class Shape
    {
       public abstract string AsString();
    }
    public class Circle : Shape
    {
        private float radius;

        public Circle() : this(1.0f)
        {
            
        }

        public Circle(float radius)
        {
            this.radius = radius;
        }

        public override string AsString() => $"A Circle with radius {radius}";

        public void resize(float factor)
        {
            radius *= factor;
        }

    }
    public class Square : Shape
    {
        public Square() : this(0.0f)
        {
            
        }
        private float side;

        public Square(float side)
        {
            this.side = side;
        }

        public override string AsString() => $"A square with side {side}";
    }

    public class ColoredShape : Shape
    {
        private Shape shape;
        private string color;

        public ColoredShape(Shape shape, string color)
        {
            this.shape = shape ?? throw new ArgumentNullException(nameof(shape));
            this.color = color ?? throw new ArgumentNullException(nameof(color));
        }


        public override string AsString()
        {
            return $"{shape.AsString()} has the color of {color}";
        }
    }

    public class ColoredShape<T> : Shape 
        where T : Shape , new()
    {
        private string color;
        T shape = new T();

        public ColoredShape() : this("black")
        {
            
        }

        public ColoredShape(string color)
        {
            this.color = color ?? throw new InvalidOperationException();
        }

        public override string AsString() => $"{shape.AsString()} has the color of {color}";
        
    }
    public class TransparentShape : Shape
    {
        private Shape shape;
        private float transperency;

        public TransparentShape(Shape shape, float transperency)
        {
            this.shape = shape;
            this.transperency = transperency;
        }

        public override string AsString() => $"{shape.AsString()} has transperency of {transperency * 100.0}%";

    }
    public class TransparentShape<T> : Shape
       where T : Shape, new()
    {
        private float transparency;
        T shape = new T();


        public TransparentShape() : this(0.0f)
        {

        }

        public TransparentShape(float transparency)
        {
            this.transparency = transparency;
        }

        public override string AsString() => $"{shape.AsString()} has the transparency of {this.transparency}";

    }
    internal class DyanmicDC
    {
        static void Main(string[] args)
        {
            var redSquare = new ColoredShape<Square>();
            Console.WriteLine(redSquare.AsString());

            var circle = new TransparentShape<ColoredShape<Circle>>();
            Console.WriteLine(circle.AsString());


        }
    }
}
