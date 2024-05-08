namespace Memento
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

        public BankAcount(int balance)
        {
            this.balance = balance;
            Console.WriteLine("created bank acount with {0} dollars in balance", balance);
        }
        public Memento Deposite(int amount)
        {
            balance += amount;
            Console.WriteLine("addin {0} to bank acount",amount);
            return new Memento(balance);
        }
        public void Restore(Memento m)
        {
            balance = m.Balance;
            Console.WriteLine($"restoring bank acount to previous state with {m.Balance} dollars in balance");
        }
        public override string? ToString()
        {
            return $"{nameof(balance)} : {balance}";
        }
    }
    internal class MementoP
    {
        static void Main(string[] args)
        {
            var ba = new BankAcount(100);

            var m1 =ba.Deposite(20); // 200
            var m2 = ba.Deposite(100); //300
            Console.WriteLine(ba);

            ba.Restore(m1);
            Console.WriteLine(ba);



        }
    }
}
