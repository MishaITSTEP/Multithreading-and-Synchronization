using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
namespace TaskManeger.ViewModel
{
    public partial class FullInfoOfProcess:Window
    {
        public ObservableCollection<Process> ProcessInfo { get; set; }
        public FullInfoOfProcess()
        {
            InitializeComponent();
            this.DataContext=this;
        }
    }
}