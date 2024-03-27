using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for NavigationBar.xaml
    /// </summary>
    public partial class NavigationBar : UserControl
    {
        public NavigationBar()
        {
            InitializeComponent();
            IsManager();
            IsUSer();
        }

        public async Task IsManager()
        {
            if (Login.LoggedInUsers.IsManager)
            {
                SubscriptionsLabel.Visibility = Visibility.Visible;
            }
        }
        public async Task IsUSer()
        {
            if (!Login.LoggedInUsers.IsManager)
            {
                SubscriptionsLabel.Visibility = Visibility.Collapsed;
            }
        }
        private void HomeLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Login.LoggedInUsers.IsManager)
                NavigationService.GetNavigationService(this).Navigate(new AdminHomePage());
            else
                NavigationService.GetNavigationService(this).Navigate(new HomePage());
        }

        private void SingersLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new SingersTube4You());
        }


        private void SongsLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new SongsTube4You());
        }

        private void playlistsLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new MyPlaylists());
        }

        private void SubscriptionsLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new SubscriptionsList());
        }
    }
}
