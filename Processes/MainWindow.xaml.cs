using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace TaskManeger
{
    public partial class MainWindow:Window
    {
        public MyProcesses MyProcess { get; set; } = new MyProcesses();
        public ObservableCollection<string> RefreshTime { get; set; } = new ObservableCollection<string>(Enumerable.Range(1,15).Select(x => x.ToString()));
        public MainWindow()
        {
            RefreshTime.Insert(0,"Manual");
            InitializeComponent();
            this.DataContext=this.MyProcess;
            RefreshTimeCB.ItemsSource=RefreshTime;
            RefreshTimeCB.SelectedIndex=0;
        }
        private void Close_Btn(object sender,RoutedEventArgs e) => MyProcess.Close();
        private void FullInfo_Btn(object sender,RoutedEventArgs e) => MyProcess.ShowFullInfo();
        private void Refresh_Btn(object sender,RoutedEventArgs e) => MyProcess.Update();
        private void ComboBox_SelectionChanged(object sender,System.Windows.Controls.SelectionChangedEventArgs e) => MyProcess.SetTimerUpdate((sender as ComboBox).SelectedIndex);
        private void OpenFile_Btn(object sender,RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog()==true)
                Process.Start(openFileDialog.FileName);
        }
    }
}
