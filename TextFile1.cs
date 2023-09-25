using PropertyChanged;
using System.Threading;
using System.Threading.Tasks;
namespace Task_Manager
{
    [AddINotifyPropertyChangedInterface]
    public class Ticker
    {
        public int N { get; private set; } = 0;
        private bool IsRun { get; set; } = false;
        //public DateTime ___________________a { get; private set; }
        //private static CancellationTokenSource CancellationTokenSource { get; set; } = new CancellationTokenSource();
        //private static CancellationToken CancellationToken { get; set; }
        public Ticker()
        {
            //Stopwatch stopwatch = new Stopwatch();
            if(!IsRun)
            {
                //CancellationToken= CancellationTokenSource.Token;
                //IsRun = true;
                Task.Run(() =>
                {
                    while(true)
                    {
                        //___________________a = DateTime.UtcNow;
                        Thread.Sleep(100);
                        N++;
                    }
                }
                //, CancellationToken
                );
            }
        }
    }
}
