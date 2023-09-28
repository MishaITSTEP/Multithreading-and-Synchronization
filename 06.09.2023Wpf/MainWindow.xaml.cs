using System.Windows;

namespace _06._09._2023Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <= 50; i++)

                lb2.Items.Add(i.ToString());
        }
    }
}
