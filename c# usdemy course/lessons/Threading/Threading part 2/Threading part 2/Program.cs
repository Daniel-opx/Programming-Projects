namespace Threading_part_2
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            
           TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>(); // Tak completion source is a generic class
            //TaskCompletionSource is class that uses object if type Task (also class))
            var thread = new Thread(() =>
            {
                Console.WriteLine($"Thread number: {Thread.CurrentThread.ManagedThreadId} started");//uses static method to get the instance of the 
               // thread and operates on the instance , in this case use getter prop of ManagedThreadId- get thread id 
                Thread.Sleep(10000/2);
                Console.WriteLine(taskCompletionSource.TrySetResult(true)); // attemps to change the Task object to done status and return boolean
                Console.WriteLine($"Thread number: {Thread.CurrentThread.ManagedThreadId} ended");

            });
            
            thread.Start();
            var test = await taskCompletionSource.Task; // hte Task propery gets the Task<T> that created by
            //the TaskCompletionSource, the resault property gets you the resault value of the task
           // that is of the same type as the Task
            Console.WriteLine("task was done:{0}",test);
            

            //==============================================================//
            //                    part 2 - Thread Pools                     //
            Enumerable.Range(0, 1000).ToList().ForEach(_ =>   //provides a set of static methods to 
                                                             //query objects that implemet IEnumerable interface. Range- generates a sequence of 
                                                             // integral numbers within a rang - IEnumerable<int>
            {
                ThreadPool.QueueUserWorkItem(urlString => //thread pool is litteraly a bunch of threads
                                                  //the QueueUserWorkItem queues a method to execute when a thread from the pool becomes
                                                  //availabe. the method is described with lamda

                {
                    var url = urlString as string;
                    myDownloadLogic.download(url);
                    Console.WriteLine($"Thread number: {Thread.CurrentThread.ManagedThreadId} started");
                    Thread.Sleep(1000);

                    Console.WriteLine($"Thread number: {Thread.CurrentThread.ManagedThreadId} ended");
                }, "https://google.com");
            });
            /*ther is s alot of threads to create here and the thread pool handles it 
             but it cost us with time.
            if we use threads we need to control them, thread pool wait until the next thread is ready 
            or one thread is done.
            when you use threads you usually do something in the background and in the front end
            - ui.so everythong that happens in front , in the ui, happens in the main thread and everything
            eles like manging data, downloading data should be done in the background otherwise the 
            ui will be frozen because all the threads will be busy 
            with backgrond things.*/

            /*the managed threads in the threadpool is background threads, ie the 
             * the thread isBackground property is true, in the thread pool queue there are only 
             * background threads. */
            //we can define the is background prop manually in yhe following way:
            new Thread(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("bla");
            })
            { IsBackground = true }.Start();
            
            
                
                
           
            
        }
    }
}
