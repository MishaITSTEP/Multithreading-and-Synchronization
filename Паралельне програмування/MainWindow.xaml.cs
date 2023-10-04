using System.Windows;

namespace Паралельне_програмування
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

        private void AnalisisButton_Click(object sender,RoutedEventArgs e)
        {
            ViewModel.Analis();
        }
    }
}
