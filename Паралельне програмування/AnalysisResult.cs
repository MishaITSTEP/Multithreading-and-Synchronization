using PropertyChanged;
using System;

namespace Паралельне_програмування
{
    [AddINotifyPropertyChangedInterface]
    [Serializable]
    public class AnalysisResult
    {
        public string Text { get; set; } = "";
        public AnalysisResult() { }

        public int Sentences { get; set; } = 0;
        public int InterrogativeSentences { get; set; } = 0;
        public int ExclamatorySentences { get; set; } = 0;
        public int Characters { get; set; } = 0;
        public int Words { get; set; } = 0;
        public string ToString => string.Join("\n",
            $"\n Number of sentences: {Sentences}",
            $"\n Number of interrogative sentences: {InterrogativeSentences}",
            $"\n Number of exclamatory sentences: {ExclamatorySentences}",
            $"\n Number of characters: {Characters}",
            $"\n Number of words: {Words}",
            $""
            );
    }
}