namespace ObserverViaSPecialInterfaces
{
    public class Event
    {
        
    }
    public class FallsIllEvent : Event
    {
        public string Address;
    }
    public class Person : IObservable<Event>
    {
        private readonly HashSet<Subscription> subscriptions = new HashSet<Subscription>();
        public IDisposable Subscribe(IObserver<Event> observer)
        {
            var subscribtion = new Subscription(this, observer);
            subscriptions.Add(subscribtion);
            return subscribtion;
        }
        public void FallIll()
        {
            foreach (var s in subscriptions)
            {
                s.Observer.OnNext(new FallsIllEvent() { Address = "ta[uah0" });
            }
        }
        private class Subscription : IDisposable
        {
            private readonly Person person;
            public readonly IObserver<Event> Observer;
            public Subscription(Person person , IObserver<Event> observer)
            {
                this.person = person;
                this.Observer = observer;
            }
            public void Dispose()
            {
                person.subscriptions.Remove(this);
            }
        }
    }
    internal class InterfacesObserverP : IObserver<Event>
    {
        static void Main(string[] args)
        {
            //Iobserver , Iobservable
            new InterfacesObserverP();
        }
        public InterfacesObserverP()
        {
            var person =new Person();
            IDisposable sub = person.Subscribe(this);
            person.FallIll();

            
            
        }

        public void OnCompleted()
        {
           //mehod that will be called when there are no event the can be generated
        }

        public void OnError(Exception error)
        {
            //ignore 
        }

        public void OnNext(Event value)
        {
            if(value is FallsIllEvent args)
                Console.WriteLine($"a doctor is reauiered at {args.Address}");
        }
    }
}
