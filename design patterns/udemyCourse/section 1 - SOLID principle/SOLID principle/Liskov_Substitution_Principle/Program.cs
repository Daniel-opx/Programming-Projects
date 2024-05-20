namespace Liskov_Substitution_Principle
{
    //this priciple dictates the you should be able to upcast your 
    // class instanciation to the more generic class without masssing with the 
    // functionality of the code
    //public class Rectengle
    ///*this Rectengle class do not follow the Liskov_Substitution_Principle because of the new keyword in the square class that inherit from 
    //  the rectengle class - it ignores the class that it inherites from and thus cnat use polymorphic properties
    //*/
    //{
    //    public int Width { get; set; }
    //    public int Height { get; set; }

    //    public Rectengle() 
    //    {

    //    }
    //    public Rectengle(int width, int height)
    //    {
    //        Width = width;
    //        Height = height;
    //    }
    //    public override string ToString()
    //    {
    //        return $"{nameof(Width)} : {Width} , {nameof(Height)} : {Height}";
    //    }
    //}
    //public class Square : Rectengle
    //{
    //    public new int Width
    //    {
    //        set { base.Width = base.Height = value; }
    //    }
    //    public new int Height
    //    {
    //        set { base.Height = base.Width = value; }
    //    }
    //}
    public class Rectengle
    /*this Rectengle class do  follow the Liskov_Substitution_Principle because its using the virtual keyword in the rectengle class and override 
     * and that way it can be polymorphic propery of using the specific implemntation of methods and properies of the derived class i.e square class ,while being casted as the more general 
     * class i.e rectengale class
    */
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectengle()
        {

        }
        public Rectengle(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public override string ToString()
        {
            return $"{nameof(Width)} : {Width} , {nameof(Height)} : {Height}";
        }
    }
    public class Square : Rectengle
    {
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }
        public override int Height
        {
            set { base.Height = base.Width = value; }
        }
    }
    internal class Program
    {
        public static int CalcArea(Rectengle r) => r.Width * r.Height;

        static void Main(string[] args)
        {
            var r = new Rectengle(5, 10);
            Console.WriteLine($"the rectengle name is {nameof(r)} ,{r}\n" +
                $"its area is {CalcArea(r)} ");

            Rectengle sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"the name of square is {nameof(sq)}\n" +
                $"dimensions: {sq} , Area:{CalcArea(sq)}");
        }
    }
}
