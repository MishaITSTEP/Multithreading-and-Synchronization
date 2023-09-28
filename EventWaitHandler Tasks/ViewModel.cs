using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;

namespace EventWaitHandler_Tasks
{

    public class ViewModel
    {

        public ViewModel() { }
        public ObservableCollection<int> run1 { get; set; } = new ObservableCollection<int>();
        public ObservableCollection<int> run2 { get; set; } = new ObservableCollection<int>();
        public ObservableCollection<int> run3 { get; set; } = new ObservableCollection<int>();
        private Mutex Mutex { get; set; } = new Mutex();

        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        private string InitFileNumbers = "Init.txt";
        private string SummFileNumbers = "Summ.txt";
        private string ProductFileNumbers = "Product.txt";

        private Random Random = new Random();
        public void Init(object obj)
        {
            if(!(obj is EventWaitHandle ev))
                return;
            //Thread.Sleep(5000);
            ev.Reset();
            Console.WriteLine($"{Thread.CurrentThread.Name}");

            Console.WriteLine("Started generating numbers");
            string text = "";
            for(int i = 0; i<5; i++)
            {
                for(int j = 0; j<2; j++)
                {
                    int tmp = Random.Next(0,99);
                    Console.Write(tmp+" ");
                    text+=tmp;
                    text+=" ";
                }
                Console.WriteLine();
                text+="\n";
            }
            using(StreamWriter writer = new StreamWriter(Path.Combine(projectDirectory,InitFileNumbers),false))
            {
                writer.WriteLine(text);
            }
            Console.WriteLine("Pairs saved!");
            ev.Set();
        }
        public void Summ(object obj)
        {
            if(!(obj is EventWaitHandle ev))
                return;
            if(ev.WaitOne()) // wait
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}");

                Console.WriteLine("Started calculating summ");
                string text = File.ReadAllText(Path.Combine(projectDirectory,InitFileNumbers));

                string newText = "";
                string[] wordArray = text.Split(new char[] { ' ','\n','\r' },StringSplitOptions.RemoveEmptyEntries);
                int[] numbers = new int[wordArray.Length];
                for(int i = 0; i<wordArray.Length; i++)
                    numbers[i]=int.Parse(wordArray[i]);
                for(int i = 0; i<numbers.Length; i=i+2)
                {
                    int tmp = numbers[i]+numbers[i+1];
                    Console.Write(tmp+" ");
                    newText+=tmp+" ";
                    Console.WriteLine();
                }
                using(StreamWriter writer = new StreamWriter(Path.Combine(projectDirectory,SummFileNumbers),false))
                {
                    writer.WriteLine(newText);
                }
                Console.WriteLine("Summ saved!");
            } else
            {
                Console.WriteLine("Thread {0} late",Thread.CurrentThread.ManagedThreadId);
            }
        }
        public void Product(object obj)
        {
            if(!(obj is EventWaitHandle ev))
                return;
            if(ev.WaitOne()) // wait
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}");

                Console.WriteLine("Started calculating mult");
                string text = File.ReadAllText(Path.Combine(projectDirectory,InitFileNumbers));

                string newText = "";
                string[] wordArray = text.Split(new char[] { ' ','\n','\r' },StringSplitOptions.RemoveEmptyEntries);
                int[] numbers = new int[wordArray.Length];
                for(int i = 0; i<wordArray.Length; i++)
                    numbers[i]=int.Parse(wordArray[i]);
                for(int i = 0; i<numbers.Length; i=i+2)
                {
                    int tmp = numbers[i]*numbers[i+1];
                    Console.Write(tmp+" ");
                    newText+=tmp+" ";
                }
                using(StreamWriter writer = new StreamWriter(Path.Combine(projectDirectory,ProductFileNumbers),false))
                {
                    writer.WriteLine(newText);
                }
                Console.WriteLine("Mult saved!");
            } else
            {
                Console.WriteLine("Thread {0} late",Thread.CurrentThread.ManagedThreadId);
            }
        }

        public void Start()
        {
            ManualResetEvent waitHandler = new ManualResetEvent(true);

            Thread InitThread = new Thread(Init);
        }
    }
}
