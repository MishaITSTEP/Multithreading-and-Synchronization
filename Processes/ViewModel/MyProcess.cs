using System;
using System.Diagnostics;
namespace TaskManeger
{
    //[AddINotifyPropertyChangedInterface]
    public class MyProcess
    {
        public MyProcess(Process process)
        {
            try
            { Process=process; } catch { }
            try
            { Id=process.Id; } catch { }
            try
            { ProcessName=process.ProcessName; } catch { }
            try
            { StartInfo=process.StartInfo.ToString(); } catch { }
            try
            { BasePriority=process.BasePriority; } catch { }
            try
            { PriorityClass=process.PriorityClass.ToString(); } catch { }
            //try { } catch { }
        }
        public Process Process { get; private set; }
        public int Id { get; private set; } = -1;
        public string ProcessName { get; private set; } = "Null";
        public DateTime StartTime { get; private set; } = new DateTime();
        public string StartInfo { get; set; } = "";
        public int BasePriority { get; set; } = -1;
        public string PriorityClass { get; set; } = "";
    }
}
