namespace threading_lesson__1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            new Thread(() => /* the Thread class ctor can accept delgate
                              * that return void and accept nothing as an 
                              * argument, thts what we passed here as a lambda
                              * expression*/
            { 
                Thread.Sleep(1000);
                Console.WriteLine("Thread 1");

            }).Start();  //start method changes the state of the thread to running, 
                         // simply executed what is within the curly brackets
            new Thread(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Thread 2");
            }).Start();
            new Thread(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Thread 3");
            }).Start();
            new Thread(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Thread 4");
            }).Start();
            

        }
    }
}
