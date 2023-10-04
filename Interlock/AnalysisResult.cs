using PropertyChanged;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Interlock
{
    [AddINotifyPropertyChangedInterface]
    public class AnalysisResult
    {
        public bool IsReading { get; private set; } = false;
        public string Text { get; set; } = "";
        public string Path { get; set; } = "";
        public bool TextShow { get; set; }
        public AnalysisResult() { }
        public string Error { get; set; } = "The file size is larger than the allowable value!!!";
        public AnalysisResult(string @path = "")
        {
            new Thread(ReadFileText,0) { IsBackground=true }.Start(@path);
        }

        private void ReadFileText(object path)
        {

            Monitor.Enter(this);
            if(!(path is string path1))
                return;
            Path=path1;
            try
            {
                int MaxSize =
                    //1024*1024;
                    1024*1024*32;
                if(new FileInfo(path1).Length>=MaxSize)
                    return;
                Text=File.ReadAllText(path1);
                IsReading=true;
            } catch(Exception e) { Error=e.Message; } finally
            {
                Monitor.Exit(this);
            }

        }

        public int Words => Text.Split(" \t\n\r".ToArray(),StringSplitOptions.RemoveEmptyEntries).Count();
        public int Lines => Text.Split('\n').Count();
        public int Symbols => Text.Count();
        public int Punctuation => Regex.Matches(Text,@"[.,;:–—‒…!?""''«»(){}[\]<>\/]").Count;
        public string ToString =>
            $"Words: {Words}\n"+
            $"Lines: {Lines}\n"+
            $"Symbols: {Symbols}\n"+
            $"Punctuation: {Punctuation}";
    }
}