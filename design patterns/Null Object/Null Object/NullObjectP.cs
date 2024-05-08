using System.ComponentModel;

namespace Null_Object
{
    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);
    }

    public class ConsoleLog : ILog
    {
        void ILog.Info(string msg)
        {
            Console.WriteLine(msg);
        }

        void ILog.Warn(string msg)
        {
            Console.WriteLine($"Warning : {msg}");
        }
    }


    public class BankAcount
    {
        ILog log;
        private int balance = default(int);
        
        public BankAcount(ILog log, int balance)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.balance = balance;
        }
        public BankAcount(ILog log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public void Deposite(int amount)
        {
            this.balance += amount;
            log.Info($"Deposited {amount} , balnace is now {balance}");
        }
        public override string ToString() => $"this bank acount has {balance}.";
        

    }
    public class NullLog : ILog // so in null obejct we create a object that conform to the interface but the interface methods implemntation soes nothing.
    {
        void ILog.Info(string msg)
        {
           
        }

        void ILog.Warn(string msg)
        {
            
        }
    }
    internal class NullObjectP
    {
        static void Main(string[] args)
        {
            var log = new NullLog();
            var ba = new BankAcount(log);
            Console.WriteLine(ba);
           
            ba.Deposite(1005); Console.WriteLine(ba);
        }
    }
}
