using System.Windows;

namespace Home06._09._2023WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow:Window
    {
        public ViewModel viewModel = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext=viewModel;
        }

        private void ForseStop(object sender,RoutedEventArgs e)
        {
            viewModel.ForseStop();
        }

        private void Replay(object sender,RoutedEventArgs e)
        {
            viewModel.Replay();
        }

        private void Start(object sender,RoutedEventArgs e)
        {
            viewModel.Start();
        }
    }
}
