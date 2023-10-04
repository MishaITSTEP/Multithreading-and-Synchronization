using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace _08._09._2023WPF
{
    public partial class MainWindow:Window
    {
        public ObservableCollection<Factorial> operations { get; set; } = new ObservableCollection<Factorial>(Enumerable.Range(1,5).Select(x => new Factorial(Enumerable.Range(1,x).Aggregate((x,z) => x*z))));
        public int value { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext=this;
            //foreach(Factorial f in operations)
            //    ThreadPool.QueueUserWorkItem(f.FindFactorial, f.Value);
        }
        private void Button_Click(object sender,RoutedEventArgs e)
        {
            Factorial info = new Factorial(value);
            //ThreadPool.QueueUserWorkItem(info.FindFactorial, info.Value);
            operations.Add(info);
        }
    }
}
