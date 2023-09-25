internal class Program
{
    private static void Main()
    {
        const int cmin = 0;
        const int cmax = 17;
        #region 1
        int[] ints = Enumerable.Range(cmin, cmax).ToArray();


        Parallel.Invoke(
            () => { Parallel.For(cmin, cmax, (val) => Factorial(val)); },
            () => { Parallel.For(cmin, cmax, (val) => CountNumbers(val)); },
            () => { Parallel.For(cmin, cmax, (val) => SummNumbers(val)); }
            );
        //Parallel.For(cmin, cmax, (val) => { Factorial(val); CountNumbers(val); SummNumbers(val); });
        //Parallel.ForEach(ints, Factorial);
        #endregion


        #region 2

        #endregion


        #region 3
        string strings = "Text.txt";

        int min = 1;
        int max = 10;
        File.Delete(strings);
        using(StreamWriter writer = new(strings, true))
        {
            Parallel.For(min, max, (x) =>
            {
                AWriteToFile(writer, x);
            });
        }
        #endregion


        #region 4
        string string1 = "Text1.txt";
        File.Delete(string1);
        using(StreamWriter writer = new(string1, true))
        {
            for(int i = 0; i < 10; i++)
            {
                writer.WriteLine(i);
            }
        }

        if(!File.Exists(string1))
            File.Create(string1);
        using(StreamReader streamReader = new(string1))
        {
            var Chars = Enumerable.Range(char.MinValue, char.MaxValue).Select(x => (char)x).Where(x => !Char.IsDigit(x)).ToArray();

            var arr = streamReader.ReadToEnd().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));
            //Parallel.ForEach(arr, Factorial);
            arr.AsParallel().AsOrdered().ForAll((x) => Factorial(x));
        }
        #endregion


        #region 5

        #endregion



    }
    static void AWriteToFile(StreamWriter Path, int x)
    {
        for(int y = 1; y <= 10; y++)
        {
            lock(Path)
                Path.WriteLine($"{x}*{y} = {x * y}");
            Thread.Sleep(1);
            Console.WriteLine($"{new string('\t', x)}{x}*{y} = {x * y}");
        }

    }
    public static void Factorial(int A) => Console.WriteLine($"Factorial {A,3} : {Enumerable.Range(1, A >= 1 ? A : 1).Select(x => (Int128)x).Aggregate((z, x) => z * x)}");
    public static void CountNumbers(int Val) => Console.WriteLine($"\t\t\t\tCount in [{Val}]: {(Val > 0 ? Val : -Val).ToString().Length,-10}");
    public static void SummNumbers(int Val) => Console.WriteLine($"\t\t\t\t\t\t\tSum in [{Val}]: {(Val > 0 ? Val : -Val).ToString().Select(x => (int)x).Sum(),-10}");

}