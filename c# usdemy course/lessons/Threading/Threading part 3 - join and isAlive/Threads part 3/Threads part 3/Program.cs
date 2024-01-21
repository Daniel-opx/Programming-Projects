namespace Threads_part_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread started");
            var thread1 = new Thread(ThreadFunction);
            var thread2 = new Thread(ThreadFunction2);
            thread1.Start();
            thread2.Start();

           
            

            if (thread1.Join(1000)) // bool that checks if the thread in question ended within 
                                  //the given time (in miliseconds) in the parameter, if not it return false, 
                                  // in the saem time it invokes the join method and tell the compiler to 
                                  //wait and block the caller thread (the main method in this case)
                                  //for the given time in the parameter - 1000 miliseconds or 1 second
            {
                Console.WriteLine("thread function 1 was done ");
            }
            else
            {
                Console.WriteLine("Thread function 1 wasnt done in 1 second");
            }
            thread2.Join();
            Console.WriteLine("thread function 2 ended");//this method blocks the calling Thread, in this case 
                                                         //the main thread, until the instance method,i.e the thread1 , terminates


            for (int i = 0; i < 10; i++)
            {
                if (thread1.IsAlive) // checks if the thread is still running
                {
                    Console.WriteLine("thread 1 is still running");
                    Thread.Sleep(300);
                }
                else
                {
                    Console.WriteLine("Thread 1 completed");
                }
            }
            
            


            Console.WriteLine("Main Thread ended");
        }

        public static void ThreadFunction()

        {

            Console.WriteLine("Thread function 1 started");
            Thread.Sleep(3000); // Thread.sleep is a static method that waits for number of seconds
            Console.WriteLine("Thread function 1 coming cack to caller");

        }
        public static void ThreadFunction2()
        {
            Console.WriteLine("Thread function 2 started");
        }
        public static void StartThread(params Thread[] threads)
        {
            foreach (Thread thread in threads)
            {
                thread.Start();
            }
        }
    }
}
