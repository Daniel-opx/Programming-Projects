namespace multiuple_Inheritance_with_Interfaces
{
    /*on this code we demonstrate some kind of multi inheritence through interfaces
     
     we have the classes bird and lizard , each has its unique abilty - fly and crawl respectivly, and dragon that is bird and lizard.
    inside the dragon class we have vird field and lizard field ,both implement its corresponding interface - IBird and ILizard and dragon class implements both
    */
    public interface IBird
    {
        public void Fly();
    }

    public interface ILizard
    {
        public void Crawl();
    }
    public class Bird : IBird
    {
        public void Fly()
        {
            Console.WriteLine("Soaring in the sky");
        }
    }
    public class Lizard : ILizard
    {
        public void Crawl()
        {
            Console.WriteLine("Crawling in the dirt");
        }
    }
    public class Dragon : IBird , ILizard
    {
        private Bird bird = new Bird();
        private Lizard lizard = new Lizard();

        public void Crawl()
        {
            lizard.Crawl();
        }

        public void Fly()
        {
            bird.Fly();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var d = new Dragon();
            d.Crawl(); d.Fly();
        }
    }
}
