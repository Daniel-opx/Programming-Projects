namespace Open_Closed_Principle
{
    /*the open-c;osed principle dictates that a class should be open for extension but closed fro modification.
     it is done through inheritence - interfaces
    the code that down here - the productfilter class that offer a couple of method that filter the products via different filters - doesnot follow that principle , 
    if we want to extent the class and add more filters we need to modify the class itself*/
    public enum Color
    {
        Red,
        Green,
        Blue
    }
    public enum Size
    {
        Small,
        Medium,
        Large,
        Yuge
    }
    public class Product
    {
        public string Name;
        public Color Color;
        public Size size;

        public Product(string name, Color color, Size size)
        {
            if (name == null) throw new ArgumentNullException(paramName: nameof(name));
            this.Name = name;
            this.Color = color;
            this.size = size;
        }
    }

    //this productfilter class doesnt follow the open-closed principle.
    public class ProductFilter
    {
        public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (Product p in products)
            {
                if (p.size == size) yield return p;
            }
        }
        public static IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (Product p in products)
            {
                if (p.Color == color) yield return p;
            }
        }

        public static IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Color color, Size size)
        {
            foreach (Product p in products)
            {
                if (p.Color == color && p.size == size) yield return p;
            }
        }
    }





    //interface that implement a method in which you return bool and put the type of the generic as a param
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }
    //method in which the paramaters are IEnumerable of T , and interface that provide the filter predicate
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }
    //this method implement the ISpecification interface .this is by color
    public class ColorSpecification : ISpecification<Product>
    {
        private Color color; // variable to keep the Color enum


        public ColorSpecification(Color color) // ctor that takes COlor as an arg and asign is in the color field of the class
        {
            this.color = color;
        }
        public bool IsSatisfied(Product t) //  simple bool that compares the property that given by the user in the ctor - the color - to the property of the T object
        {
            return t.Color == color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;
        public SizeSpecification(Size size)
        {
            this.size = size;
        }
        public bool IsSatisfied(Product t)
        {
            return t.size == size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            if (first == null) throw new ArgumentNullException(paramName: nameof(first));
            if (second == null) throw new ArgumentNullException(paramName: nameof(second));

            this.first = first;
            this.second = second;
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }



    //this method comines all this together -  it implemnts the Ifilter interface (this interface implement a method in which one of the arguments is any class that implements the ISpecification interface)
    //it it itterates over the IEnumerable of the products and thakes the Ispecification implementing class - in this case the ColorSpecification class -  and checks if the current p in prodcts IEnumerable
    // statisfy the bool of the Color specification
    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var p in items)
            {
                if (spec.IsSatisfied(p)) yield return p;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("apple", Color.Green, Size.Small);
            var tree = new Product("tree", Color.Green, Size.Large);
            var house = new Product("house", Color.Blue, Size.Large);
            Product[] products = new Product[] { apple, tree, house };

            foreach (Product p in ProductFilter.FilterBySize(products, Size.Large))
            {
                Console.WriteLine($"- {p.Name} is Large");
            }

            foreach (Product p in ProductFilter.FilterBySizeAndColor(products, Color.Green, Size.Small))
            {
                Console.WriteLine($"name = {p.Name} , size = {p.size} , color = {p.Color}");
            }


            // this block of code uses the interfaces and the method in such way that you can extent the filter methods without modifing the methods itself. open-closed principle
            var betterFilter = new BetterFilter();
            Console.WriteLine("now using better filter:");
            foreach (Product p in betterFilter.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($"name = {p.Name} , size = {p.size} , color = {p.Color}");
            }

            Console.WriteLine("Large BLue items:");

            foreach (var p in betterFilter.Filter(products,
                new AndSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large))))
            {
                Console.WriteLine($"name: {p.Name}");

            }
        }
    }
}
