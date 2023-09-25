using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;
using System.Threading;

namespace Interlock
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ViewModel
    {
        public ObservableCollection<AnalysisResult> AnalysisResult { get; set; } = new ObservableCollection<AnalysisResult>();
        public int A { get; set; } = 0;
        public ViewModel()
        {
            SelectFile_Click(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        }

        public void SelectFile_Click(string path = null)
        {
            Monitor.Enter(this);
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if(string.IsNullOrEmpty(path))
            {
                if(folderBrowserDialog.ShowDialog()!=DialogResult.OK)
                    return;
            } else
            {
                folderBrowserDialog.SelectedPath=path;
            }
                AnalysisResult.Clear();


            foreach(var item in Directory.GetFiles(folderBrowserDialog.SelectedPath,"*.*"))
            {
                try
                {
                    var a = new AnalysisResult(item);
                    AnalysisResult.Add(a);
                } catch(Exception e) { MessageBox.Show(e.Message); }
            }
            Monitor.Exit(this);
        }
    }
}
