internal class Program
{
    internal static void Main()
    {
        ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        MyClass MyClass = new();

        new Thread(MyClass.Generation).Start(manualResetEvent);
        new Thread(MyClass.Addition).Start(manualResetEvent);
        new Thread(MyClass.Multiplication).Start(manualResetEvent);
        Thread.Sleep(5000);


    }
}

public class MyClass
{

    private string InitFileNumbers = "Generation.txt";
    private string SummFileNumbers = "Addition.txt";
    private string ProductFileNumbers = "Multiplication.txt";

    private Random Random = new Random();
    public void Generation(object obj)
    {
        if(!(obj is EventWaitHandle ev))
            return;
        ev.Reset();
        var id = GetId(Thread.GetCurrentProcessorId());

        Console.WriteLine($"{id}Start Generation {Thread.GetCurrentProcessorId()}");
        {
            string t = "";
            for(int j = 0; j<20; j++)
            {
                var a = Enumerable.Range(0,3).Select(x => Random.Next(100));

                var r = string.Join(" ",a);
                t+=r+'\n';
                Console.WriteLine($"{id}{r}");
            }
            lock(InitFileNumbers)
            {
                File.WriteAllText(InitFileNumbers,t);
            }
        }
        Console.WriteLine($"{id}End Generation");
        ev.Set();
    }
    public void Addition(object obj)
    {
        if(!(obj is EventWaitHandle ev))
            return;
        var id = GetId(Thread.GetCurrentProcessorId());
        if(ev.WaitOne())
        {
            Console.WriteLine($"{id}Start Addition {Thread.GetCurrentProcessorId()}");
            string text;
            lock(InitFileNumbers)
            { text=File.ReadAllText(Path.Combine(InitFileNumbers)); }
            string t = "";

            var wordArray = text.Split("\r\n".ToArray(),StringSplitOptions.RemoveEmptyEntries);
            foreach(var item in wordArray)
            {
                var c = item.Split(' ').Select(x => int.Parse(x));
                var r = $"{c.Select(x => x.ToString()).Aggregate((z,x) => $"{z}+{x}")}={c.Aggregate((z,x) => z+x)}";
                t+=r+'\n';
                Console.WriteLine($"{id}{r}");
            }
            File.WriteAllText(SummFileNumbers,t);
            Console.WriteLine($"{id}End Addition");
        } else
        {
            Console.WriteLine($"{id}Addition don't run");
        }
    }



    public void Multiplication(object obj)
    {
        if(!(obj is EventWaitHandle ev))
            return;
        var id = GetId(Thread.GetCurrentProcessorId());
        if(ev.WaitOne())
        {
            Console.WriteLine($"{id}Start Multiplication {Thread.GetCurrentProcessorId()}");
            string text;
            lock(InitFileNumbers)
            { text=File.ReadAllText(Path.Combine(InitFileNumbers)); }
            string t = "";

            var wordArray = text.Split("\r\n".ToArray(),StringSplitOptions.RemoveEmptyEntries);
            foreach(var item in wordArray)
            {
                var c = item.Split(' ').Select(x => int.Parse(x));
                var r = $"{c.Select(x => x.ToString()).Aggregate((z,x) => $"{z}*{x}")}={c.Aggregate((z,x) => z*x)}";
                t+=r+'\n';
                Console.WriteLine($"{id}{r}");
            }
            File.WriteAllText(ProductFileNumbers,t);
            Console.WriteLine($"{id}End Multiplication");
            ev.Reset();
        } else
        {
            Console.WriteLine($"{id}Multiplication don't run");
        }
    }
    private string GetId(int n) => new string(' ',n*4);
}
