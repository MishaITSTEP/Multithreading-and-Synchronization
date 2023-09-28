using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;
using TaskManeger.ViewModel;
namespace TaskManeger
{
    [AddINotifyPropertyChangedInterface]
    public class MyProcesses
    {
        public DispatcherTimer Timer = new DispatcherTimer();
        public MyProcess SelectProc { get; set; }
        public int Inner { get; private set; } = 0;
        public int Complete { get; private set; } = 0;
        public int Count { get; set; } = 15;
        public int All => Proc.Count;
        public string Prog => $"Inner: {Inner,10}, All: {All,10}";
        public ObservableCollection<MyProcess> Proc { get; set; } = new ObservableCollection<MyProcess>();
        public MyProcesses()
        {
            Timer.Tick+=Tick;
            Update();
        }
        public void SetTimerUpdate(int k)
        {
            if(k==-1)
                return;
            if(k==0)
            {
                if(Timer.IsEnabled)
                {
                    Timer.Stop();
                }
            }
            if(k>=1)
            {
                if(Timer.IsEnabled)
                    Timer.Stop();
                Timer.Interval=TimeSpan.FromSeconds(k);
                Timer.Start();
            }
        }
        private void Tick(object sender,System.EventArgs e) => Update();
        public void Update()
        {
            Complete=0;
            var NewColl = new ObservableCollection<MyProcess>(
                Count<0
                ? Process.GetProcesses().Select(x => new MyProcess(x))
                : Process.GetProcesses().Take(Count).Select(x => new MyProcess(x)));
            Proc=NewColl;
            Inner++;
        }
        public void Close()
        {
            SelectProc.Process.CloseMainWindow();
            Proc.Remove(SelectProc);
        }
        public void ShowFullInfo()
        {
            if(SelectProc!=null)
                new FullInfoOfProcess() { ProcessInfo=new ObservableCollection<Process>() { SelectProc.Process } }.Show();
        }
    }
}
