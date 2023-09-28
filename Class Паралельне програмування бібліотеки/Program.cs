internal class Program
{
    private static void Main(string[] args)
    {
        #region 1
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token = cancelTokenSource.Token;
        int millisecondsTimeout = 1000;
        Task[] tasks = new[] {
        new Task(() =>
        {
            while (!token.IsCancellationRequested)
            {
                Console.WriteLine($"{DateTime.Now,-30}Start");
                Thread.Sleep(millisecondsTimeout);
            }
        }, token),
        Task.Run(() =>
        {
            while (!token.IsCancellationRequested)
            {
                Console.WriteLine($"{DateTime.Now,-30}Run");
                Thread.Sleep(millisecondsTimeout);
            }
        }, token),
        Task.Factory.StartNew(() =>
        {
            while (!token.IsCancellationRequested)
            {
                Console.WriteLine($"{DateTime.Now,-30}StartNew");
                Thread.Sleep(millisecondsTimeout);
            }
        }, token)
        };
        tasks[0].Start();
        Console.ReadKey();
        cancelTokenSource.Cancel();
        //Task.WaitAll(tasks);
        #endregion


        #region 2
        Task.Run(() =>
        {
            for (int i = 1; i < 100; i++)
            {
                bool tf = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        tf = false;
                    }
                }
                if (tf)
                {
                    Console.WriteLine(i);
                }
            }
        }).Wait();
        #endregion


        #region 3
        Console.WriteLine("Enter min limit value: ");
        int min = 99;
        //min = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter max limit value: ");
        int max = 999;
        //max = int.Parse(Console.ReadLine());
        Task.Run(() =>
        {
            Prime(max, min);
        }).Wait();
        #endregion


        #region 4
        var arr = Enumerable.Range(0, 20).Select(x => new Random().Next(100)).ToList<int>();
        Console.WriteLine($"Arr: {string.Join(", ", arr)}");

        min = Task.Run<int>(() =>
        {
            int val = arr.First();
            foreach (var item in arr)
            {
                if (item < val)
                    val = item;
            }
            return val;
        }).Result;
        Console.WriteLine($"{min,-10}:{arr.Min(),-10}Min");

        max = Task.Run<int>(() =>
        {
            int val = arr.First();
            foreach (var item in arr)
            {
                if (item > val)
                    val = item;
            }
            return val;
        }).Result;
        Console.WriteLine($"{max,-10}:{arr.Max(),-10}Max");

        var avg = Task.Run<double>(() =>
        {
            int val = 0;
            foreach (var item in arr)
            {
                val += item;
            }
            return (double)val / arr.Count();
        }).Result;
        Console.WriteLine($"{avg,-10}:{arr.Average(),-10}Avg");

        var sum = Task.Run<int>(() =>
        {
            int val = 0;
            foreach (var item in arr)
            {
                val += item;
            }
            return val;
        }).Result;
        #endregion

        #region 5
        Console.WriteLine($"Arr: {string.Join(", ", arr)}");

        Task.Run(() =>
        {
            arr = arr.Distinct().ToList();
            Thread.Sleep(1000);
            Console.WriteLine("<Distinct>");
        }).Wait();
        Console.WriteLine($"Arr: {string.Join(", ", arr)}");

        Task.Run(() =>
        {
            arr = arr.Order().ToList();
            Thread.Sleep(1000);
            Console.WriteLine("<Order>");
        }).Wait();
        Console.WriteLine($"Arr: {string.Join(", ", arr)}");

        Console.WriteLine("Enter value: ");
        int val = int.Parse(Console.ReadLine());
        Console.WriteLine($"Value inndex: {Task.Run<int>(() =>
                                           arr.IndexOf(val)
        ).Result}");
        #endregion
    }
    static void Prime(int Max = 100, int Min = 1)
    {
        Min = Math.Max(1, Min);
        Max = Math.Max(Min, Max);
        for (int i = Min; i < Max; i++)
        {
            bool tf = true;
            for (int j = 2; j < i; j++)
            {
                if (i % j == 0)
                {
                    tf = false;
                }
            }
            if (tf)
            {
                Console.WriteLine(i);
            }
        }
    }
}