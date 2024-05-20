namespace Null_Object_Singleton
{
    interface ILog
    {
        public void Warn();

        public static ILog Null => NullLog.Instance;

        private sealed class NullLog : ILog
        {
            private NullLog() { }

            private static Lazy<NullLog> instance =
              new Lazy<NullLog>(() => new NullLog());

            public static ILog Instance => instance.Value;

            public void Warn()
            {

            }
        }
    }
    internal class BankAccount
    {
        private ILog log;
        public BankAccount(ILog log)
        {
            this.log = log;
        }
    }




    internal class NullObjectSingleton
    {
        static void Main(string[] args)
        {
            ILog log = ILog.Null;
            Console.WriteLine("Hello, World!");
            var bal =int.MaxValue;
        }
    }
}
