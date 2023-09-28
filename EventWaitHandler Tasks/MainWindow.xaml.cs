using System.Windows;

namespace EventWaitHandler_Tasks
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow:Window
    {
        public ViewModel ViewModel { get; set; } = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext=this.ViewModel;
        }

        private void Start_Btn(object sender,RoutedEventArgs e)
        {
            ViewModel.Start();
        }
    }
}
