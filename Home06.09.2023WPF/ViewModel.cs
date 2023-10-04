using PropertyChanged;
using System;
using System.Threading;
namespace Home06._09._2023WPF
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModel
    {
        public int timeSleep { get; set; } = 10;
        public int TimeSleep
        {
            get => timeSleep;
            set => timeSleep=int.Max(int.Min(value,1000),0);
        }
        public int First { get; set; } = 0;
        public int End { get; set; } = 1024;
        public int Progress => Math.Min(ProgressPrime,ProgressFibo);
        public bool IsEmpty { get; set; } = true;
        public bool IsFull => !IsEmpty;
        public ViewModel() { }
        #region 1
        public int Prime { get; set; } = 1;
        public bool IsEmpty_Prime { get; set; } = true;
        public int ProgressPrime { get; set; } = 0;
        private void Task_1()
        {
            IsEmpty_Prime=false;
            First=First>=2 ? First : 2;
            End=End>First ? End : First+1;
            for(int i = First; i<=End; i++)
            {
                bool tf = true;
                lock(this)
                {
                    if(IsEmpty)
                        break;
                    for(int j = 2; j<i; j++)
                    {
                        if(i%j==0)
                        {
                            tf=false;
                            break;
                        }
                    }
                    if(tf)
                        Prime=i;
                }
                Thread.Sleep(timeSleep);
                ProgressPrime=(int)(((float)i-First)/((float)End-First)*100);
            }
            IsEmpty_Prime=true;
            Check();
        }
        #endregion
        #region 2
        public void Start_2() => new Thread(Task_2) { IsBackground=true }.Start();
        public int Fibonachi { get; set; } = 1;
        public bool IsEmpty_Fibo { get; set; } = true;
        public int ProgressFibo { get; set; } = 0;
        private void Task_2()
        {
            Fibonachi=0;
            IsEmpty_Fibo=false;
            First=First>=2 ? First : 2;
            End=End>First ? End : First+1;
            int previu = 0;
            int next = 1;
            while(true)
            {
                lock(this)
                {
                    if(IsEmpty)
                        break;
                    if(Fibonachi+previu<=End)
                    {
                        ProgressFibo=100;
                        break;
                    }
                    Fibonachi=previu+next;
                    previu=next;
                    next=Fibonachi;
                }
                Thread.Sleep(timeSleep);
                ProgressFibo=(int)(((float)Fibonachi-First)/((float)End-First)*100);
            }
            IsEmpty_Fibo=true;
            Check();
        }
        #endregion
        #region 
        public void Start_1() => new Thread(Task_1) { IsBackground=true }.Start();
        internal void Start()
        {
            Thread thread = new Thread(Task_1) { IsBackground=true };
            IsEmpty=false;
            if(IsEmpty_Prime)
                thread.Start();
            if(IsEmpty_Fibo)
                Start_2();
        }
        #endregion
        #region 3
        internal void ForseStop() => IsEmpty=true;
        private void Check() { if(IsEmpty_Fibo&&IsEmpty_Prime) IsEmpty=true; }
        #endregion
        #region 5
        internal void Replay()
        {
            ForseStop();
            while(!(IsEmpty_Fibo&&IsEmpty_Prime))
            {
                Thread.Sleep(100);
            }
            IsEmpty=true;
            Start();
        }
        #endregion
    }
}
