using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Interlock
{
    public partial class MainWindow:Window
    {
        public ViewModel ViewModel { get; set; } = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext=this.ViewModel;
        }

        private void SelectFile_Click(object sender,RoutedEventArgs e)
        {
            ViewModel.SelectFile_Click();
        }
    }

}