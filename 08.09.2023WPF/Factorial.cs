using PropertyChanged;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Data;

namespace _08._09._2023WPF
{
    [AddINotifyPropertyChangedInterface]
    public class Factorial
    {

        public int Progress { get; set; } = 0;//perseent
        public bool IsProgress { get; set; } = true;//try
        public float Number { get; set; } = 1;//to
        //public string aaaaaaaaaaa { get; set; } = new string('e',10);
        public float Value { get; set; } = 0;//this [i]
        public decimal Result { get; set; } = 1;//end
        public double dResult { get; set; } = 1;//end
        public Factorial(int number)
        {
            Number = number;
            ThreadPool.QueueUserWorkItem(FindFactorial);
        }

        private void FindFactorial(object obj)
        {
            try
            {
                if(Number <= 1)
                {
                    Progress = 100;
                    return;
                }
                for(int i = 1; i <= Number; i++)
                {
                    Thread.Sleep(50);
                    Value = i;
                    Result *= i;
                    Progress = (int)(100f / (Number / Value));
                }
            }
            catch(Exception)
            {
                IsProgress = false;
            }
        }

    }
}
