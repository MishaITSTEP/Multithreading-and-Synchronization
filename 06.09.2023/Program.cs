using System.Reflection;

namespace _06._09._2023
{
    public class Program
    {
        public class THRange
        {
            public int start = 0;
            public int end;

            public THRange(int start, int end)
            {
                this.start = start;
                this.end = end;
            }
        }
        public class THRangeAndCount
        {
            public int start = 0;
            public int end;
            public int count;

            public THRangeAndCount(int start, int end, int count = 1)
            {
                this.start = start;
                this.end = end;
                this.count = count;
            }
        }
        static void Method1()
        {
            for (int i = 0; i <= 50; i++)
                Console.WriteLine(i);
        }

        static void Method2(object range)
        {
            var r = (THRange)range;
            for (int i = r.start; i <= r.end; i++)
                Console.WriteLine(i);
        }
        static void Method3(object range)
        {
            var r = (THRangeAndCount)range;
            if (r.count != 0)
            {
                Thread th = new Thread(Method3);
                th.Start((new THRangeAndCount(r.start, r.end, r.count - 1)));
            }
            for (int i = r.start; i <= r.end; i++)
            {
                Thread.Sleep(10);
                Console.WriteLine(i);
            }
        }

        static void Method4Max(object array) => Console.WriteLine((array as List<int>)?.Max() ?? Int32.MaxValue);
        static void Method4Min(object array) => Console.WriteLine((array as List<int>)?.Min() ?? Int32.MinValue);
        static void Method4Avg(object array) => Console.WriteLine((array as List<int>)?.Average() ?? 0);
        private static void Main(string[] args)
        {
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    new Thread(Method1).Start();
                    break;
                case 2:
                    new Thread(Method2).Start((new THRange(0, 50)));
                    break;
                case 3:
                    new Thread(Method3).Start((new THRangeAndCount(0, 50, 5)));
                    break;
                case 4:
                    {
                        Thread[] threads =
           {
                new Thread(Method4Max),
                new Thread(Method4Min),
                new Thread(Method4Avg)
            };
                        var arr = Enumerable.Range(0, 10000);
                        foreach (var item in threads)
                            item.Start(arr);
                    }
                    break;
                case 5:
                    {
                    }
                    break;
                case 6:
                    {
                    }
                    break;
                case 7:
                    {
                    }
                    break;

                default:
                    break;
            }
        }
    }
}