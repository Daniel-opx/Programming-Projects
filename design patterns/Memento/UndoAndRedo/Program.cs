namespace UndoAndRedo
{
    public class Memento
    {
        public int Balance { get; }
        public Memento(int balance)
        {
            this.Balance = balance;
        }
    }
    public class BankAcount
    {

        private int balance;
        private List<Memento> changes = new List<Memento>();
        private int current;
        public BankAcount(int balance)
        {
            this.balance = balance;
            changes.Add(new Memento(balance));
            current = 0;
            //Console.WriteLine("created bank acount with {0} dollars in balance", balance);
        }
        public Memento Deposite(int amount)
        {
            balance += amount;
            Console.WriteLine("addin {0} to bank acount", amount);
            var m = new Memento(balance);
            changes.Add(m);
            current++;
            return m;
        }
        public void Restore(Memento m)
        {
            if(m != null)
            {
                balance = m.Balance;
                changes.Add(m);
                
            }
          
            
        }
        public Memento Undo()
        {
            if(current > 0)
            {
                var m = changes[--current];
                balance = m.Balance;
                return m;
            }
            return null;
        }
        public Memento Redo()
        {
            if(current + 1 < changes.Count)
            {
                var m = changes[++current];
                balance = m.Balance;
                return m;
            }
            return null;
        }

        public override string? ToString()
        {
            return $"{nameof(balance)} : {balance}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var ba = new BankAcount(100);
             ba.Deposite(25);
             ba.Deposite(50);
            Console.WriteLine(ba);

            ba.Undo();
            Console.WriteLine("undo 1 : {0}",ba);
            ba.Undo();
            Console.WriteLine($"undo 2: {ba}");
            Console.WriteLine("now redoing {0}",ba);
            ba.Redo();
            Console.WriteLine(ba);
        }
    }
}
