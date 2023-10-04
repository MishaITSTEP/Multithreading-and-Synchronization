using System.Windows;

namespace Sync_Client
{
    public partial class MainWindow:Window
    {
        public ViewModelClient ViewModelClient { set; get; } = new ViewModelClient();

        public MainWindow()
        {
            this.DataContext=this.ViewModelClient;
            InitializeComponent();

        }

        private void Button_Click(object sender,RoutedEventArgs e)
        {
            ViewModelClient.Send();
        }
    }
}
