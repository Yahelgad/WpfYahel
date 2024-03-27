using System.Windows;
using System.Windows.Controls;
using YahelApiService;
namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame MFrame;
        YahelApiSrv api = new();

        public MainWindow()
        {
            InitializeComponent();
            MFrame = this.MainFrame;

            Closing += MainWindow_Closing;
        }

        private async void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}