using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;

namespace TaskManeger
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow:Window
    {
        public List<Process> processes { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            processes = Process.GetProcesses().Take(10).ToList();
            tt.ItemsSource = processes;
            //tt.ItemsSource = processes.Select(x => x.ToString());
            dd.ItemsSource = processes;
            //this.DataContext = ints;
            Thread.Sleep(new TimeSpan(0, 0, 10));
            this.Close();
        }
        //public async Task<int> asd()
        //{ return await Task.Run(() => { return (int)1; }); }
    }
}
