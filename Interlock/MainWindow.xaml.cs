using System.Windows;

namespace Interlock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel ViewModel { get; set; } = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this.ViewModel;
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectFile_Click();
        }
    }
}
