public class Program
{
    //public static int q = 0;
    static public Semaphore semaphore = new Semaphore(3, 3);
    private static void Main(string[] args)
    {
        for(int i = 0; i < 10; i++)
        {
            //ThreadPool.QueueUserWorkItem(Download, i);
            new Thread(Download).Start(i + 1);
        }
    }
    static public void Download(object? obj)
    {
        int n = (int)obj;
        //lock (typeof(Program))
        //{
        //    n = q;
        //    q++;
        //}

        semaphore.WaitOne();
        for(int i = 0; i < 10; i++)
        {
            Console.WriteLine($"{new string('\t', n)}#{n,-2}|{new Random().Next(10)}");
            Thread.Sleep(50);
        }
        Console.WriteLine($"Thread #{n} Id:{Thread.CurrentThread.ManagedThreadId} Complete");
        semaphore.Release();
    }
}