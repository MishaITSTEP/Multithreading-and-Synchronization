namespace _06._09._2023
{
   public static class NewChar
    {
        struct MyStruct
        {
            public int id;
            public char ch;
            public override string? ToString() => $"{ch}:{id}";

        }
        public static void NewMethod()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            var CH = Enumerable.Range(0, Char.MaxValue).Select(x => new MyStruct() { id = x, ch = (char)x });
            Console.WriteLine(string.Join("", CH.Select(x => x.ch)));
            string l;
            do
            {
                l = Console.ReadLine();
                var ch = l.Select(x => CH.FirstOrDefault(y => x == y.id));
                var ch1 = ch.Select(x => x.ToString());
                Console.WriteLine(string.Join("\n", ch1));
                Console.WriteLine(string.Join("", ch.Select(x => x.ch)));
            }
            while (l != "");
        }
    }
}
