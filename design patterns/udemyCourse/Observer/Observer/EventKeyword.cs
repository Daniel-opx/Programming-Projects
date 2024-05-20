namespace Observer
{
    public class FallsIllEventArgs : EventArgs
    {
        public string Address;
    }
    public class Person
    {
        public void CatchACold() => FallesIll?.Invoke(this,new FallsIllEventArgs() { Address = "tapuh 3"});
        public event EventHandler<FallsIllEventArgs> FallesIll;
    }
    internal class EventKeyword
    {
        static void Main(string[] args)
        {
           var person = new Person();
            person.FallesIll += CallDoctor;
            person.CatchACold();
            person.FallesIll-= CallDoctor;
        }

        

        private static void CallDoctor(object sender, FallsIllEventArgs e)
        {
            Console.WriteLine($"A doctor will come to patient {nameof(e.Address)}: {e.Address}");
        }
    }
}
