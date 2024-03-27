using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for LogOrRegister.xaml
    /// </summary>
    public partial class LogOrRegister : Page
    {
        public LogOrRegister()
        {
            InitializeComponent();
        }


        private void loginClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Login());
        }

        private void registerClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Register());
        }
    }
}
