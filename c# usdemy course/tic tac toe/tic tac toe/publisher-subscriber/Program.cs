using System.Threading.Channels;

namespace publisher_subscriber
{
    public class Publisher
    {
        public event EventHandler myEvent;
        public void RaiseEvent()
        {
            if(myEvent != null) myEvent(this, EventArgs.Empty);
        }
    }

    public class Subscriber
    {
        public static int lambdaNum = 0;
        private Publisher publisher;
        public delegate void myDel (object? sender, EventArgs e);

        public Subscriber(Publisher publisher)
        {
            publisher.myEvent += MyEventHandler;
        }

        private void MyEventHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Event handled by " + sender.GetType().Name);
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var pub  = new Publisher();
            var sub = new Subscriber(pub);

            pub.RaiseEvent();

            
        }

        public static void CallingDelegate(object sender, EventArgs e)
        {
            Console.WriteLine($"the sender of this lambda is {nameof(Program)}");
        }
    }
}
